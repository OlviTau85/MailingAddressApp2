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

namespace MailAdsApp.BLL.Services
{
    public class MailAdsTableService: IMailAdsTableService
    {
        IUnitOfWork DataBase { get; set; }

        public MailAdsTableService(IUnitOfWork uow)
        {
            DataBase = uow;
        }

        public IEnumerable<MailAddressDTO> GetMailAddresses()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<MailAddress, MailAddressDTO>();
            });
            return config.CreateMapper().Map<IEnumerable<MailAddress>, List<MailAddressDTO>>(DataBase.MailAddresses.GetAll());
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
