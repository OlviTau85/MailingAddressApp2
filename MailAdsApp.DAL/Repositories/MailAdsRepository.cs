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
    public class MailAdsRepository: IRepository<MailAddress>
    {
        private MailAdsContext db;

        public MailAdsRepository(MailAdsContext context)
        {
            this.db = context;
        }
        public IEnumerable<MailAddress> GetAll()
        {
            return db.MailAddresses;
        }
    }
}
