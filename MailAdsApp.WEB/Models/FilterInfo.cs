using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailAdsApp.WEB.Models
{
    public class FilterInfo
    {
        public string CountryFilter { get; set; }
        public string CityFilter { get; set; }
        public string StreetFilter { get; set; }
        public int MinHouseNumberFilter { get; set; }
        public int MaxHouseNumberFilter { get; set; }
        public int FromHouseNumberFilter { get; set; }
        public int UntilHouseNumberFilter { get; set; }
        public string IndexFilter { get; set; }
        public DateTime MinCreationDate { get; set; }
        public DateTime MaxCreationDate { get; set; }
        public DateTime FromCreationDate { get; set; }
        public DateTime UntilCreationDate { get; set; }
    }
}