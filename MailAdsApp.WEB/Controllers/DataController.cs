using AutoMapper;
using MailAdsApp.BLL.DTO;
using MailAdsApp.BLL.Interfaces;
using MailAdsApp.WEB.App_Start;
using MailAdsApp.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MailAdsApp.WEB.Controllers
{
    public class DataController : Controller
    {
        IMailAdsTableService mailAdsTableService;
        MailAddressViewModel model;
        public DataController(IMailAdsTableService serv)
        {
            mailAdsTableService = serv;
            model = new MailAddressViewModel();
            model.PageInfo = new PageInfo();
        }

        //
        // GET: /Data/

        [HttpPost]
        public JsonResult GetData(PageInfo pageInfo)
        {
            IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
            model.PageInfo = pageInfo;
            model.MailAddressesList = Mapper.Map<IEnumerable<MailAddressDTO>, List<MailAddress>>(mailAdsTableService.GetMailAddresses());
            model.PageInfo.TotalItems = model.MailAddressesList.Count();
            model.MailAddressesList = model.MailAddressesList.Skip((model.PageInfo.PageNumber - 1) * model.PageInfo.PageSize).Take(model.PageInfo.PageSize);
            return new JsonResult { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult Init()
        {
            long count = mailAdsTableService.GetMailAddresses().Count();
            var filterData = new
            {
                mailAdsTableService.Filter.MinHouseNumberFilter,
                mailAdsTableService.Filter.MaxHouseNumberFilter,
                mailAdsTableService.Filter.MaxCreationDate,
                mailAdsTableService.Filter.MinCreationDate,
                count
            };
            return new JsonResult { Data = filterData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult FilterData(MailAdsApp.WEB.Models.FilterInfo filterInfo)
        {
            IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
            mailAdsTableService.Filter.CountryFilter = filterInfo.CountryFilter;
            mailAdsTableService.Filter.CityFilter = filterInfo.CityFilter;
            mailAdsTableService.Filter.StreetFilter = filterInfo.StreetFilter;
            mailAdsTableService.Filter.IndexFilter = filterInfo.IndexFilter;
            mailAdsTableService.Filter.FromHouseNumberFilter = filterInfo.FromHouseNumberFilter;
            mailAdsTableService.Filter.UntilHouseNumberFilter = filterInfo.UntilHouseNumberFilter;
            mailAdsTableService.Filter.FromCreationDate = filterInfo.FromCreationDate;
            mailAdsTableService.Filter.UntilCreationDate = filterInfo.UntilCreationDate;

            model.PageInfo.PageSize = 10;
            model.MailAddressesList = Mapper.Map<IEnumerable<MailAddressDTO>, List<MailAddress>>(mailAdsTableService.GetMailAddresses());
            
            model.PageInfo.TotalItems = model.MailAddressesList.Count();
            model.MailAddressesList = model.MailAddressesList.Skip((model.PageInfo.PageNumber - 1) * model.PageInfo.PageSize).Take(model.PageInfo.PageSize);
            return new JsonResult { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}
