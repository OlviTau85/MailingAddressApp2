using MailAdsApp.BLL.DTO;
using MailAdsApp.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MailAdsApp.DAL.Interfaces;
using MailAdsApp.DAL.Entities;
using MailAdsApp.BLL.Infrastructure;
using System.Linq.Expressions;
using System.Reflection;

namespace MailAdsApp.BLL.Services
{
    /// <summary>
    /// Service for handle data in table and work with data base
    /// </summary>
    public class MailAdsTableService: IMailAdsTableService
    {
        IUnitOfWork DataBase { get; set; }

        /// <summary>
        /// Filter settings
        /// </summary>
        public FilterInfo Filter { get; set; }

        /// <summary>
        /// Constructor for service
        /// </summary>
        /// <param name="uow">Unit of work for use it</param>
        public MailAdsTableService(IUnitOfWork uow)
        {
            DataBase = uow;
            Filter = new FilterInfo();
            IEnumerable<MailAddress> result = DataBase.MailAddresses.GetAll();
            //first filter initialization
            Filter.UntilHouseNumberFilter = Filter.MaxHouseNumberFilter = result.Max(key => key.HouseNumber);
            Filter.FromHouseNumberFilter = Filter.MinHouseNumberFilter = result.Min(key => key.HouseNumber);
            Filter.MaxCreationDate = result.Max(key => key.CreationDate);
            Filter.UntilCreationDate = result.Max(key => key.CreationDate);
            Filter.MinCreationDate = result.Min(key => key.CreationDate);
            Filter.FromCreationDate = result.Min(key => key.CreationDate);
        }

        /// <summary>
        /// Method for geting prepared filtered data
        /// </summary>
        /// <returns>Filtered collection of mail address records</returns>
        public IEnumerable<MailAddressDTO> GetMailAddresses()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<MailAddress, MailAddressDTO>();
            });
            var result = DataBase.MailAddresses.GetAll();
            if (Filter.CountryFilter != null)
            {
                result = result.Where(key => key.Country.ToLower().Contains(Filter.CountryFilter.ToLower()));
            }
            if (Filter.CityFilter != null)
            {
                result = result.Where(key => key.City.ToLower().Contains(Filter.CityFilter.ToLower()));
            }
            if (Filter.StreetFilter != null)
            {
                result = result.Where(key => key.Street.ToLower().Contains(Filter.StreetFilter.ToLower()));
            }
            if (Filter.IndexFilter != null)
            {
                result = result.Where(key => key.Index.Contains(Filter.IndexFilter));
            }
            if (Filter.FromHouseNumberFilter != Filter.MinHouseNumberFilter)
            {
                result = result.Where(key => key.HouseNumber >= Filter.FromHouseNumberFilter);
            }
            if (Filter.UntilHouseNumberFilter != Filter.MaxHouseNumberFilter)
            {
                result = result.Where(key => key.HouseNumber <= Filter.UntilHouseNumberFilter);
            }
            if (Filter.FromCreationDate != Filter.MinCreationDate)
            {
                result = result.Where(key => key.CreationDate >= Filter.FromCreationDate);
            }
            if (Filter.UntilCreationDate != Filter.MaxCreationDate)
            {
                result = result.Where(key => key.CreationDate <= Filter.UntilCreationDate);
            }
            return config.CreateMapper().Map<IEnumerable<MailAddress>, List<MailAddressDTO>>(result);
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
