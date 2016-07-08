using MailAdsApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAdsApp.DAL.Interfaces
{
    /// <summary>
    /// interface for unit of work to more usefull access to data contexts
    /// </summary>
    public interface IUnitOfWork: IDisposable
    {
        /// <summary>
        /// Repository with MailAddresses interface  
        /// </summary>
        IRepository<MailAddress> MailAddresses { get; }
        /// <summary>
        /// Method for save data
        /// </summary>
        void Save();
    }
}
