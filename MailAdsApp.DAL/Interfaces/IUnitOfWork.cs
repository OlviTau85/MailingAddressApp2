using MailAdsApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAdsApp.DAL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<MailAddress> MailAddresses { get; }
        void Save();
    }
}
