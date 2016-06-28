using MailAdsApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAdsApp.BLL.Interfaces
{
    public interface IMailAdsTableService
    {
        IEnumerable<MailAddressDTO> GetMailAddresses();
        void Dispose();
    }
}
