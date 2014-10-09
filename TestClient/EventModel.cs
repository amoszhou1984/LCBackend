using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiangchenServer
{
    public class EventModel
    {
        public int EventId { get; set; }
        public string TimeSlot { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        // {lat: -34, lng: 151}
        public string GeoString  { get; set; }
        // VIC
        public string StateProvinceCode { get; set; }
        // Use country code, e.g. CHN, AU
        public string CountryRegionCode { get; set; }
        public string EventGUID { get; set; }
    }
}