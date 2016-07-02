using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailAdsApp.WEB.Models
{
    public class MailAddressViewModel
    {
        public IEnumerable<MailAddress> MailAddressesList { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}