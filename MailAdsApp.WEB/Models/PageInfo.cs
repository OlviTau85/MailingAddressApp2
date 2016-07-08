using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailAdsApp.WEB.Models
{
    /// <summary>
    /// Page info settings for MailAddressViewModel
    /// </summary>
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; } 
        public int TotalItems { get; set; }
        public string OrderField { get; set; }
        public bool SortReverse { get; set; }
        public string CountryFilter { get; set; }
        public string CityFilter { get; set; }
        public string StreetFilter { get; set; }
        public int FromHouseNumberFilter { get; set; }
        public int UntilHouseNumberFilter { get; set; }
        public string IndexFilter { get; set; }
        public DateTime FromCreationDate { get; set; }
        public DateTime UntilCreationDate { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }

}