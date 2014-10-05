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
        public static LoginModel loginModel = new LoginModel() { username = "amoszhou@gmail.com", password = "Pa55w0rd" };
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

        static int Main(string[] args)
        {
            //string retVal = SendCreatEventRequest("http://localhost:2099/LiangChen.svc/Event/Create", "Post request content");
            string retVal = SendEventDetailRequest("http://localhost:2099/LiangChen.svc/Event/Detail", "Post request content");
            Console.WriteLine(retVal);
            return 0;
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
            postModel.ContentData = JsonConvert.SerializeObject(eventModel);
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
    }

}