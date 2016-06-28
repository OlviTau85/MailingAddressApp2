using System;

namespace MailAdsApp.DAL.Entities
{
    public class MailAddress
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string Index { get; set; }
        public System.DateTime CreationDate { get; set; }
    }
}
