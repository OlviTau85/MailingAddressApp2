using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAdsApp.DAL.Interfaces
{
    /// <summary>
    /// Interface for repository of project
    /// </summary>
    /// <typeparam name="T">Type of repository content</typeparam>
    public interface IRepository<T> where T:class
    {
        /// <summary>
        /// Method returns all data from repository
        /// </summary>
        /// <returns>Ienumerable collection of T typed data</returns>
        IEnumerable<T> GetAll();
    }
}
