using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailAdsApp.WEB.Models
{
    /// <summary>
    /// View Model for Data controller GetData action
    /// </summary>
    public class MailAddressViewModel
    {
        /// <summary>
        /// MailAddressList property maps onto BLL DTO model
        /// </summary>
        public IEnumerable<MailAddress> MailAddressesList { get; set; }
        /// <summary>
        /// PageInfo property uses for pass page info to view and back
        /// </summary>
        public PageInfo PageInfo { get; set; }
    }
}