using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.UI.WebControls;
using Newtonsoft.Json;


namespace LiangchenServer
{
    public class LiangChenServices : ILiangChenServices
    {
        private LCAuthDBEntities authDbEntities = new LCAuthDBEntities();
        private LCDB2Entities dbEntities = new LCDB2Entities();
        public string Echo(string message)
        {
            return string.Format("You entered: {0}", message);
        }


        public bool Login(Stream loginData)
        {
            // convert Stream Data to StreamReader
            var reader = new StreamReader(loginData);
            string content = reader.ReadToEnd();
            LoginModel loginModel = JsonConvert.DeserializeObject<LoginModel>(content);

            using (authDbEntities)
            {
                // Find the user from the AuthDB
                string name = loginModel.username;
                User user = authDbEntities.Users.FirstOrDefault(u => u.Email == name);
                if (user == null) return false;
                if (user.Password != loginModel.password)
                {
                    return false;
                }
                else
                {
                    // Add the user to the user pool
                    Session entry = new Session()
                    {
                        Email = name,
                        SessionID = 0,
                        StartTime = DateTime.Now,
                        IP = "tempfakeddata",
                        AccessToken = "fakedtoken",
                        Duration = 60
                    };
                    authDbEntities.Sessions.Add(entry);
                    authDbEntities.SaveChanges();
                    return true;
                }

            }
        }

        public bool CreateEvent(Stream eventData)
        {
            // First validate the user priviledges
            var reader = new StreamReader(eventData);
            string content = reader.ReadToEnd();
            LCPostModel postDataModel = JsonConvert.DeserializeObject<LCPostModel>(content);
            if (validateUser(postDataModel) == false) return false;
            using (dbEntities)
            {
                // Create Event
                EventModel anEvent = JsonConvert.DeserializeObject<EventModel>(postDataModel.ContentData);
                // Add the address
                LCStateProvince stateProvince =
                    dbEntities.LCStateProvinces.FirstOrDefault(p => p.StateProvinceCode == anEvent.StateProvinceCode
                        && p.CountryRegionCode == anEvent.CountryRegionCode);
                if (stateProvince == null) return false;
                LCAddress addressToAdd = new LCAddress()
                {
                    AddressLine1 = anEvent.AddressLine1,
                    AddressLine2 = anEvent.AddressLine2,
                    City = anEvent.City,
                    StateProvinceID = stateProvince.StateProvinceID,
                    PostalCode = anEvent.PostCode,
                    SpatialGeolocation = null //TODO string parsing needed.
                };
                dbEntities.LCAddresses.Add(addressToAdd);
                dbEntities.SaveChanges(); //TODO Address should be checked before being added

                // Add the event
                LCEvent eventToAdd = new LCEvent()
                {
                    TimeSlots = anEvent.TimeSlot,
                    Title = anEvent.Title,
                    Description = anEvent.Description,
                    AddressID = addressToAdd.AddressID
                };
                dbEntities.LCEvents.Add(eventToAdd);
                dbEntities.SaveChanges();

                // Link the creator to the event
                //We've checked this user is in the authdb, thereofore should also be in the transactional db
                var creator = dbEntities.LCUsers.FirstOrDefault(u => u.Email == postDataModel.Email);
                if (creator == null) return false;
                LCEventCreation creation = new LCEventCreation()
                {
                    EventId = eventToAdd.EventId,
                    UserId = creator.LCUserId,
                    CreationTime = DateTime.Now
                };
                dbEntities.LCEventCreations.Add(creation);
                dbEntities.SaveChanges();
            }
            return false;
        }

        private bool validateUser(LCPostModel postDataModel)
        {
            using (authDbEntities)
            {
                Session entry = authDbEntities.Sessions.FirstOrDefault(e => e.Email == postDataModel.Email
                    && e.AccessToken == postDataModel.AccessToken);
                if (entry == null)
                {
                    return false;
                }
            }
            return true;
        }

        public string ListCreatedEvents(Stream requestData)
        {
            var reader = new StreamReader(requestData);
            string content = reader.ReadToEnd();
            LCPostModel postDataModel = JsonConvert.DeserializeObject<LCPostModel>(content);
            if (validateUser(postDataModel) == false) return null;
            else
            {
                List<int> eventIds = new List<int>();
                using (dbEntities)
                {
                    var creations = dbEntities.LCEventCreations.Where(c => c.LCUser.Email == postDataModel.Email);
                    eventIds.AddRange(creations.Select(creation => creation.EventId));
                    return JsonConvert.SerializeObject(eventIds);
                }
            }
        }

        public string EventDetail(Stream requestData)
        {
            var reader = new StreamReader(requestData);
            string content = reader.ReadToEnd();
            LCPostModel postDataModel = JsonConvert.DeserializeObject<LCPostModel>(content);
            if (validateUser(postDataModel) == false) return null;
            else
            {
                int eventId = JsonConvert.DeserializeObject<int>(postDataModel.ContentData);
                LCEvent theEvent = dbEntities.LCEvents.FirstOrDefault(e => e.EventId == eventId);
                if (theEvent == null) return null;
                LCAddress address = theEvent.LCAddress;

                var eventDetails = new
                {
                    Title = theEvent.Title,
                    Description = theEvent.Description,
                    TimeSlots = theEvent.TimeSlots,
                    AddressLine1 = (address == null ? null : address.AddressLine1),
                    AddressLine2 = (address == null ? null : address.AddressLine2),
                    City = (address == null ? null : address.City),
                    StateProvinceCode = (address == null ? null : address.LCStateProvince.StateProvinceCode),
                    CountryRegionCode = (address == null ? null : address.LCStateProvince.CountryRegionCode)
                };
                return JsonConvert.SerializeObject(eventDetails);
            }
        }

        public bool EditEvent(Stream eventData)
        {
            // First validate the user priviledges
            var reader = new StreamReader(eventData);
            string content = reader.ReadToEnd();
            LCPostModel postDataModel = JsonConvert.DeserializeObject<LCPostModel>(content);
            if (validateUser(postDataModel) == false) return false;
            using (dbEntities)
            {
                // Unpack the Event
                EventModel anEvent = JsonConvert.DeserializeObject<EventModel>(postDataModel.ContentData);
                // Check whether the event exists
                LCEvent eventToUpdate = dbEntities.LCEvents.FirstOrDefault(e => e.EventId == anEvent.EventId);
                LCStateProvince stateProvince = dbEntities.LCStateProvinces.FirstOrDefault(p => p.StateProvinceCode == anEvent.StateProvinceCode
                    && p.CountryRegionCode == anEvent.CountryRegionCode); // Find the stateProvince instance matching the codes
                if (eventToUpdate == null) return false;
                eventToUpdate.Title = anEvent.Title;
                eventToUpdate.Description = anEvent.Description;
                if (eventToUpdate.LCAddress != null)
                {
                    eventToUpdate.LCAddress.AddressLine1 = anEvent.AddressLine1;
                    eventToUpdate.LCAddress.AddressLine2 = anEvent.AddressLine2;
                    eventToUpdate.LCAddress.City = anEvent.City;
                    eventToUpdate.LCAddress.PostalCode = anEvent.PostCode;
                    eventToUpdate.LCAddress.LCStateProvince = stateProvince;
                }
                dbEntities.SaveChanges();
            }
            return true;
        }

        public bool DeleteEvent(Stream eventData)
        {
            // First validate the user priviledges
            var reader = new StreamReader(eventData);
            string content = reader.ReadToEnd();
            LCPostModel postDataModel = JsonConvert.DeserializeObject<LCPostModel>(content);
            if (validateUser(postDataModel) == false) return false;
            using (dbEntities)
            {
                // Unpack the Event
                EventModel anEvent = JsonConvert.DeserializeObject<EventModel>(postDataModel.ContentData);
                // Check whether the event exists
                LCEvent eventToDelete = dbEntities.LCEvents.FirstOrDefault(e => e.EventId == anEvent.EventId);
                if (eventToDelete == null) return false;
                foreach (var creation in dbEntities.LCEventCreations.Where(creation => creation.EventId == eventToDelete.EventId))
                {
                    dbEntities.LCEventCreations.Remove(creation);
                }
                foreach (var participation in dbEntities.LCParticipations.Where(participation => participation.LCEventId == eventToDelete.EventId))
                {
                    dbEntities.LCParticipations.Remove(participation);
                }
                dbEntities.LCEvents.Remove(eventToDelete);
                dbEntities.SaveChanges();
            }
            return true;
        }

        public bool AddParticipant(Stream participantData)
        {
            throw new NotImplementedException();
        }

        public bool DelParticipant(Stream participantData)
        {
            throw new NotImplementedException();
        }

        public bool EditParticipant(Stream participantData)
        {
            throw new NotImplementedException();
        }

        public bool AddTimeslot(Stream eventData)
        {
            throw new NotImplementedException();
        }

        public bool DelTimeslot(Stream eventData)
        {
            throw new NotImplementedException();
        }

        public bool EditTimeslot(Stream eventData)
        {
            throw new NotImplementedException();
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
