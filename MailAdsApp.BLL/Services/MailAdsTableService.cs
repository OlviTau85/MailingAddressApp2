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
    public class MailAdsTableService: IMailAdsTableService
    {
        IUnitOfWork DataBase { get; set; }

        public FilterInfo Filter { get; set; }

        public MailAdsTableService(IUnitOfWork uow)
        {
            DataBase = uow;
            Filter = new FilterInfo();
            IEnumerable<MailAddress> result = DataBase.MailAddresses.GetAll();
            Filter.UntilHouseNumberFilter = Filter.MaxHouseNumberFilter = result.Max(key => key.HouseNumber);
            Filter.FromHouseNumberFilter = Filter.MinHouseNumberFilter = result.Min(key => key.HouseNumber);
            Filter.MaxCreationDate = result.Max(key => key.CreationDate);
            Filter.UntilCreationDate = result.Max(key => key.CreationDate);
            Filter.MinCreationDate = result.Min(key => key.CreationDate);
            Filter.FromCreationDate = result.Min(key => key.CreationDate);
        }

        public IEnumerable<MailAddressDTO> GetMailAddresses()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<MailAddress, MailAddressDTO>();
            });
            var result = DataBase.MailAddresses.GetAll();
            if (Filter.CountryFilter != null)
            {
                result = result.Where(key => key.Country.Contains(Filter.CountryFilter));
            }
            if (Filter.CityFilter != null)
            {
                result = result.Where(key => key.City.Contains(Filter.CityFilter));
            }
            if (Filter.StreetFilter != null)
            {
                result = result.Where(key => key.Street.Contains(Filter.StreetFilter));
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
            //result = result.ApplyOrder("Street", "OrderBy");
            return config.CreateMapper().Map<IEnumerable<MailAddress>, List<MailAddressDTO>>(result);
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
