﻿using System;
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
        public static UserModel loginModel = new UserModel() { username = "amoszhou@gmail.com", password = "Pa55w0rd" };
        public static UserModel RegModel = new UserModel()
        {
            email = "yangyuanpig@gmail.com",
            password = "1mApig",
            username = "Yang Yuan Pig",
            enabled = "true"
        };
        public static EventModel eventModel = new EventModel()
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
        public static EventModel eventModelNew = new EventModel()
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

        public static EventModel eventModelDel = new EventModel()
        {
            EventId = 4
        };

        static int Main(string[] args)
        {
            string retVal = SendJoinRequest("http://localhost:2099/LiangChen.svc/Join", "Post request content");
            //string retVal = SendEventEditRequest("http://localhost:2099/LiangChen.svc/Event/Detail", "Post request content");
            //string retVal = SendEventEditRequest("http://localhost:2099/LiangChen.svc/Event/Delete", "Post request content");

            Console.WriteLine(retVal);
            return 0;
        }

        private static string SendJoinRequest(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            Parameters = JsonConvert.SerializeObject(RegModel);
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

        public static string SendCreatEventRequest(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
            LCPostModel postModel = new LCPostModel();
            postModel.Email = "amoszhou@gmail.com";
            postModel.AccessToken = "fakedtoken";
            postModel.ContentData = JsonConvert.SerializeObject(eventModelDel);
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
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            Parameters = JsonConvert.SerializeObject(loginModel);
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
            req.ContentType = "application/x-www-form-urlencoded";
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
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            LCPostModel detailReq = new LCPostModel();
            detailReq.Email = "amoszhou@gmail.com";
            detailReq.AccessToken = "fakedtoken";
            detailReq.ContentData = JsonConvert.SerializeObject(1);
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
            req.ContentType = "application/x-www-form-urlencoded";
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

        public static string SendEventDelRequest(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            req.ContentType = "application/x-www-form-urlencoded";
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