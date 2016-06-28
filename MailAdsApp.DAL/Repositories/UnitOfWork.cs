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
    public class UnitOfWork: IUnitOfWork
    {
        private MailAdsContext db;
        private MailAdsRepository mailAdsRepository;

        public UnitOfWork(string connectionString)
        {
            db = new MailAdsContext(connectionString);
        }
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
