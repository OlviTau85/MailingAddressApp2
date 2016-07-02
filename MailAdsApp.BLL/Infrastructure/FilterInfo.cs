using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAdsApp.BLL.Infrastructure
{
    public class FilterInfo
    {
        public FilterInfo()
        {
            CountryFilter = string.Empty;
            CityFilter = string.Empty;
            StreetFilter = string.Empty;
            IndexFilter = string.Empty;
            MaxHouseNumberFilter = 0;
            MinHouseNumberFilter = 0;
            FromHouseNumberFilter = 0;
            UntilHouseNumberFilter = 0;
            MinCreationDate = DateTime.Now;
            MaxCreationDate = DateTime.Now;
            FromCreationDate = DateTime.Now;
            UntilCreationDate = DateTime.Now;
        }

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
