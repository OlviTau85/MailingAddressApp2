using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailAdsApp.DAL.Entities;

namespace MailAdsApp.DAL.EF
{
    /// <summary>
    /// Realization of data base context for Entity
    /// </summary>
    public class MailAdsContext: DbContext
    {
        /// <summary>
        /// Property for access to Mail Addreses data in data base
        /// </summary>
        /// <value>
        /// Value is MailAddress DbSet 
        /// </value>
        public DbSet<MailAddress> MailAddresses { get; set; }

        /// <summary>
        /// Constructor for context
        /// </summary>
        /// <param name="connectionString">
        /// Connection string for database connection
        /// </param>
        public MailAdsContext(string connectionString)
            : base(connectionString)
        {
        }
    }
}
