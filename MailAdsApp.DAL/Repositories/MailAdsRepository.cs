using MailAdsApp.DAL.EF;
using MailAdsApp.DAL.Entities;
using MailAdsApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAdsApp.DAL.Repositories
{
    /// <summary>
    /// Realization of Repository interface for MailAddress model
    /// </summary>
    public class MailAdsRepository: IRepository<MailAddress>
    {
        private MailAdsContext db;
        /// <summary>
        /// Constructor with context
        /// </summary>
        /// <param name="context">Context for data base access</param>
        public MailAdsRepository(MailAdsContext context)
        {
            this.db = context;
        }

        /// <summary>
        /// Get all records of MailAddress from database
        /// </summary>
        /// <returns>Collection of MailAddresses</returns>
        public IEnumerable<MailAddress> GetAll()
        {
            return db.MailAddresses;
        }
    }
}
