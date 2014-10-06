using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LiangchenServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu 
    // to change the interface name "ILiangChenServices" in both code and 
    // config file together.
    [ServiceContract]
    public interface ILiangChenServices
    {

        [OperationContract,
        WebGet(UriTemplate = "echo/{message}")]
        string Echo(string message);

        [OperationContract(Name = "Login")]
        [WebInvoke(Method = "POST", UriTemplate = "Login")]
        bool Login(Stream data);

        [OperationContract(Name = "Join")]
        [WebInvoke(Method = "POST", UriTemplate = "Join")]
        bool Join(Stream data);

        [OperationContract(Name = "CreateEvent")]
        [WebInvoke(Method = "POST", UriTemplate = "Event/Create")]
        bool CreateEvent(Stream eventData);

        [OperationContract(Name = "EditEvent")]
        [WebInvoke(Method = "POST", UriTemplate = "Event/Edit")]
        bool EditEvent(Stream eventData);

        [OperationContract(Name = "DeleteEvent")]
        [WebInvoke(Method = "POST", UriTemplate = "Event/Delete")]
        bool DeleteEvent(Stream eventData);

        [OperationContract(Name = "ListCreatedEvent")]
        [WebInvoke(Method = "POST", UriTemplate = "Event/ListCreated")]
        string ListCreatedEvents(Stream requestData);

        [OperationContract(Name = "EventDetail")]
        [WebInvoke(Method = "POST", UriTemplate = "Event/Detail")]
        string EventDetail(Stream requestData);

        [OperationContract(Name = "AddParticipant")]
        [WebInvoke(Method = "POST", UriTemplate = "Participant/Add")]
        bool AddParticipant(Stream participantData);

        [OperationContract(Name = "DelParticipant")]
        [WebInvoke(Method = "POST", UriTemplate = "Participant/Del")]
        bool DelParticipant(Stream participantData);

        [OperationContract(Name = "EditParticipant")]
        [WebInvoke(Method = "POST", UriTemplate = "Participant/Edit")]
        bool EditParticipant(Stream participantData);

        [OperationContract(Name = "AddTimeslot")]
        [WebInvoke(Method = "POST", UriTemplate = "Timeslot/Add")]
        bool AddTimeslot(Stream eventData);

        [OperationContract(Name = "DelTimeslot")]
        [WebInvoke(Method = "POST", UriTemplate = "Timeslot/Del")]
        bool DelTimeslot(Stream eventData);

        [OperationContract(Name = "EditTimeslot")]
        [WebInvoke(Method = "POST", UriTemplate = "Timeslot/Edit")]
        bool EditTimeslot(Stream eventData);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
