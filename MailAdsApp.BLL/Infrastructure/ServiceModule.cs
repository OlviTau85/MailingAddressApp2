using MailAdsApp.DAL.Interfaces;
using MailAdsApp.DAL.Repositories;
using Ninject.Modules;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace MailAdsApp.BLL.Infrastructure
{
    /// <summary>
    /// Class for initialaze dependency injection into services
    /// </summary>
    public class ServiceModule: NinjectModule
    {
        private string connectionString;

        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        /// <summary>
        /// Loads all bindings for project
        /// </summary>
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
