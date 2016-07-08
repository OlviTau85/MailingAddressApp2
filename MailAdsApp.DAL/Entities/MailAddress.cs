using System;

namespace MailAdsApp.DAL.Entities
{
    /// <summary>
    /// MailAddress Model Code first Description
    /// </summary>
    public class MailAddress
    {
        /// <summary>
        /// Primary key Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Country property
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// City property
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Street property
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// House number property
        /// </summary>
        public int HouseNumber { get; set; }
        /// <summary>
        /// Index property
        /// </summary>
        public string Index { get; set; }
        /// <summary>
        /// Creation date property
        /// </summary>
        public System.DateTime CreationDate { get; set; }
    }
}
