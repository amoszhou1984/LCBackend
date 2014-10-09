using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using LiangchenServer;
using Newtonsoft.Json;


namespace TestClient
{
    public class Program
    {
        public static UserModel userModelLogin = new UserModel()
        {
            email = "sirenyao@gmail.com",
            password = "1mApig"
        };

        public static UserModel userModelLogout = new UserModel()
        {
            email = "sirenyao@gmail.com"
        };

        public static UserModel userModelJoin = new UserModel()
        {
            email = "sirenyao@gmail.com",
            password = "1mApig",
            username = "Yang Yuan Pig",
            enabled = "true"
        };
        public static EventModel eventModelNew = new EventModel()
        {
            TimeSlot = "",
            Title = "Amos's Test",
            Description = "Test creation of an event",
            AddressLine1 = "1/19 Pacific Hwy",
            AddressLine2 = "",
            City = "Melbourne",
            PostCode = "4122",
            GeoString = null,
            StateProvinceCode = "VIC",
            CountryRegionCode = "AUS"
        };
        public static EventModel eventModelUpdate = new EventModel()
        {
            EventId = 4,
            TimeSlot = "",
            Title = "Amos's Test Updated",
            Description = "Test update of an event",
            AddressLine1 = "19 Pacific Hwy",
            AddressLine2 = "",
            City = "Sydney",
            PostCode = "4122",
            GeoString = null,
            StateProvinceCode = "NSW",
            CountryRegionCode = "AUS"
        };

        public static EventModel eventModelDetail = new EventModel()
        {
            EventId = 4
        };


        public static EventModel eventModelDel = new EventModel()
        {
            EventId = 4
        };

        static int Main(string[] args)
        {
            string retVal = null;
            //string uri = "liangchenapp.com:808";
            string uri = "localhost:2099";
            retVal = SendJoinRequest("http://" + uri + "/LiangChen.svc/User/Join", "Join request");
            Console.WriteLine(retVal);
            retVal = SendLoginRequest("http://" + uri + "/LiangChen.svc/User/Login", "Login request");
            Console.WriteLine(retVal);
            retVal = SendLogoutRequest("http://" + uri + "/LiangChen.svc/User/Logout", "Logout request");
            Console.WriteLine(retVal);
            retVal = SendCreatEventRequest("http://" + uri + "/LiangChen.svc/Event/Create", "Create request");
            Console.WriteLine(retVal);
            retVal = SendLoginRequest("http://" + uri + "/LiangChen.svc/User/Login", "Login request");
            Console.WriteLine(retVal);
            retVal = SendCreatEventRequest("http://" + uri + "/LiangChen.svc/Event/Create", "Create request");
            Console.WriteLine(retVal);
            retVal = SendListEventRequest("http://" + uri + "/LiangChen.svc/Event/ListCreated", "ListEvent request");
            Console.WriteLine(retVal);
            retVal = SendEventDetailRequest("http://" + uri + "/LiangChen.svc/Event/Detail", "GetEventDetail request");
            Console.WriteLine(retVal);
            retVal = SendEventEditRequest("http://" + uri + "/LiangChen.svc/Event/Edit", "Edit content");
            Console.WriteLine(retVal);
            retVal = SendListEventRequest("http://" + uri + "/LiangChen.svc/Event/ListCreated", "ListEvent request");
            Console.WriteLine(retVal);
            retVal = SendEventDetailRequest("http://" + uri + "/LiangChen.svc/Event/Detail", "GetEventDetail request");
            Console.WriteLine(retVal);
            retVal = SendEventDelRequest("http://"+uri+"/LiangChen.svc/Event/Delete", "EventDelete request");
            Console.WriteLine(retVal);
            retVal = SendListEventRequest("http://"+uri+"/LiangChen.svc/Event/ListCreated", "ListEvent request");
            Console.WriteLine(retVal);
            return 0;
        }

        private static string SendLogoutRequest(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            req.ContentType = "json";
            req.Method = "POST";
            LCPostModel postModel = new LCPostModel();
            //To logout, the user must be logged in
            postModel.Email = "amoszhou@gmail.com";
            postModel.AccessToken = "fakedtoken";
            Parameters = JsonConvert.SerializeObject(postModel);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        private static string SendJoinRequest(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            req.ContentType = "json";
            req.Method = "POST";
            LCPostModel postModel = new LCPostModel();
            postModel.ContentData = JsonConvert.SerializeObject(userModelJoin);
            Parameters = JsonConvert.SerializeObject(postModel);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        public static string SendCreatEventRequest(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            req.ContentType = "json";
            req.Method = "POST";
            //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
            LCPostModel postModel = new LCPostModel();
            postModel.Email = "amoszhou@gmail.com";
            postModel.AccessToken = "fakedtoken";
            postModel.ContentData = JsonConvert.SerializeObject(eventModelNew);
            Parameters = JsonConvert.SerializeObject(postModel);
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        public static string SendLoginRequest(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            req.ContentType = "json";
            req.Method = "POST";
            LCPostModel postModel = new LCPostModel();
            // No need to validate the user -- simply read the user login and password right away
            postModel.ContentData = JsonConvert.SerializeObject(userModelLogin);
            Parameters = JsonConvert.SerializeObject(postModel);
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        public static string SendListEventRequest(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            req.ContentType = "json";
            req.Method = "POST";
            LCPostModel listReq = new LCPostModel();
            listReq.Email = "amoszhou@gmail.com";
            listReq.AccessToken = "fakedtoken";
            Parameters = JsonConvert.SerializeObject(listReq);
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        public static string SendEventDetailRequest(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            req.ContentType = "json";
            req.Method = "POST";
            LCPostModel detailReq = new LCPostModel();
            detailReq.Email = "amoszhou@gmail.com";
            detailReq.AccessToken = "fakedtoken";
            detailReq.ContentData = JsonConvert.SerializeObject(eventModelDetail);
            Parameters = JsonConvert.SerializeObject(detailReq);
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        public static string SendEventEditRequest(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            req.ContentType = "json";
            req.Method = "POST";
            LCPostModel eventEditRequest = new LCPostModel();
            eventEditRequest.Email = "amoszhou@gmail.com";
            eventEditRequest.AccessToken = "fakedtoken";
            eventEditRequest.ContentData = JsonConvert.SerializeObject(eventModelDetail);
            Parameters = JsonConvert.SerializeObject(eventEditRequest);
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        public static string SendEventDelRequest(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            req.ContentType = "json";
            req.Method = "POST";
            LCPostModel eventEditRequest = new LCPostModel();
            eventEditRequest.Email = "amoszhou@gmail.com";
            eventEditRequest.AccessToken = "fakedtoken";
            eventEditRequest.ContentData = JsonConvert.SerializeObject(eventModelDel);
            Parameters = JsonConvert.SerializeObject(eventEditRequest);
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }
    }

}