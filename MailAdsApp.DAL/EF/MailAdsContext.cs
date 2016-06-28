using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailAdsApp.DAL.Entities;

namespace MailAdsApp.DAL.EF
{
    public class MailAdsContext: DbContext
    {
        public DbSet<MailAddress> MailAddresses { get; set; }

        public MailAdsContext(string connectionString)
            : base(connectionString)
        {
        }
    }
}
