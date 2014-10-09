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

        //E.g. http://liangchenapp.com:808/LiangChen.svc/echo/somemessage
        [OperationContract,
        WebGet(UriTemplate = "echo/{message}", ResponseFormat = WebMessageFormat.Json)]
        string Echo(string message);

        //E.g. http://liangchenapp.com:808/LiangChen.svc/Login
        //Returns the access token
        [OperationContract(Name = "Login")]
        [WebInvoke(Method = "POST", UriTemplate = "User/Login", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string Login(Stream data);

        //E.g. http://liangchenapp.com:808/LiangChen.svc/Logout
        //Returns whether the operation is successful
        [OperationContract(Name = "Logout")]
        [WebInvoke(Method = "POST", UriTemplate = "User/Logout", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool Logout(Stream userData);

        //E.g. http://liangchenapp.com:808/LiangChen.svc/Join
        [OperationContract(Name = "Join")]
        [WebInvoke(Method = "POST", UriTemplate = "User/Join", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string Join(Stream data);

        //E.g. http://liangchenapp.com:808/LiangChen.svc/Event/Create
        [OperationContract(Name = "CreateEvent")]
        [WebInvoke(Method = "POST", UriTemplate = "Event/Create", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool CreateEvent(Stream eventData);

        //E.g. http://liangchenapp.com:808/LiangChen.svc/Event/Edit
        [OperationContract(Name = "EditEvent")]
        [WebInvoke(Method = "POST", UriTemplate = "Event/Edit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool EditEvent(Stream eventData);

        //E.g. http://liangchenapp.com:808/LiangChen.svc/Event/Delete
        [OperationContract(Name = "DeleteEvent")]
        [WebInvoke(Method = "POST", UriTemplate = "Event/Delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool DeleteEvent(Stream eventData);

        //E.g. http://liangchenapp.com:808/LiangChen.svc/Event/ListCreated
        [OperationContract(Name = "ListCreatedEvent")]
        [WebInvoke(Method = "POST", UriTemplate = "Event/ListCreated", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string ListCreatedEvents(Stream requestData);

        //E.g. http://liangchenapp.com:808/LiangChen.svc/Event/Detail
        [OperationContract(Name = "EventDetail")]
        [WebInvoke(Method = "POST", UriTemplate = "Event/Detail", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string EventDetail(Stream requestData);

        [OperationContract(Name = "AddParticipant")]
        [WebInvoke(Method = "POST", UriTemplate = "Participant/Add", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool AddParticipant(Stream participantData);

        [OperationContract(Name = "DelParticipant")]
        [WebInvoke(Method = "POST", UriTemplate = "Participant/Del", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool DelParticipant(Stream participantData);

        [OperationContract(Name = "EditParticipant")]
        [WebInvoke(Method = "POST", UriTemplate = "Participant/Edit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool EditParticipant(Stream participantData);

        [OperationContract(Name = "AddTimeslot")]
        [WebInvoke(Method = "POST", UriTemplate = "Timeslot/Add", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool AddTimeslot(Stream eventData);

        [OperationContract(Name = "DelTimeslot")]
        [WebInvoke(Method = "POST", UriTemplate = "Timeslot/Del", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool DelTimeslot(Stream eventData);

        [OperationContract(Name = "EditTimeslot")]
        [WebInvoke(Method = "POST", UriTemplate = "Timeslot/Edit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
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
