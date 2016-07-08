using MailAdsApp.DAL.EF;
using MailAdsApp.DAL.Entities;
using MailAdsApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAdsApp.DAL.Repositories
{
    /// <summary>
    /// Realization of Unit of work interface for project
    /// </summary>
    public class UnitOfWork: IUnitOfWork
    {
        private MailAdsContext db;
        private MailAdsRepository mailAdsRepository;

        /// <summary>
        /// Constructor with connection string parametr
        /// </summary>
        /// <param name="connectionString">Conection string to data base</param>
        public UnitOfWork(string connectionString)
        {
            db = new MailAdsContext(connectionString);
        }
        /// <summary>
        /// Property for access to repository with mail address data
        /// </summary>
        public IRepository<MailAddress> MailAddresses
        {
            get 
            {
                if (mailAdsRepository == null)
                {
                    mailAdsRepository = new MailAdsRepository(db);
                }
                return mailAdsRepository;
            }
        }

        /// <summary>
        /// Method for save info into database
        /// </summary>
        public void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
