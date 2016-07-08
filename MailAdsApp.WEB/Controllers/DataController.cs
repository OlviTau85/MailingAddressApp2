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
    /// <summary>
    /// DataController as simple API
    /// </summary>
    public class DataController : Controller
    {
        IMailAdsTableService mailAdsTableService;
        public DataController(IMailAdsTableService serv)
        {
            mailAdsTableService = serv;
        }

        //
        // GET: /Data/

        /// <summary>
        /// POST action for create page with table of mailaddress data? filtered and sorted
        /// </summary>
        /// <param name="pageInfo">PageInfo settings</param>
        /// <returns>model consists of prepared list of mail addreses and some page info</returns>
        [HttpPost]
        public JsonResult GetData(PageInfo pageInfo)
        {
            IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
            MailAddressViewModel model = new MailAddressViewModel();
            model.PageInfo = pageInfo;
            // setup filters
            mailAdsTableService.Filter.CountryFilter = model.PageInfo.CountryFilter;
            mailAdsTableService.Filter.CityFilter = model.PageInfo.CityFilter;
            mailAdsTableService.Filter.StreetFilter = model.PageInfo.StreetFilter;
            mailAdsTableService.Filter.IndexFilter = model.PageInfo.IndexFilter;
            mailAdsTableService.Filter.FromHouseNumberFilter = model.PageInfo.FromHouseNumberFilter;
            mailAdsTableService.Filter.UntilHouseNumberFilter = model.PageInfo.UntilHouseNumberFilter;
            mailAdsTableService.Filter.FromCreationDate = model.PageInfo.FromCreationDate;
            mailAdsTableService.Filter.UntilCreationDate = model.PageInfo.UntilCreationDate;
            model.MailAddressesList = Mapper.Map<IEnumerable<MailAddressDTO>, List<MailAddress>>(mailAdsTableService.GetMailAddresses());
            // and check up ordering
            switch (model.PageInfo.OrderField)
            {
                case "Country":
                    model.MailAddressesList = model.PageInfo.SortReverse ? model.MailAddressesList.OrderByDescending(key => key.Country) : model.MailAddressesList.OrderBy(key => key.Country);
                    break;
                case "City":
                    model.MailAddressesList = model.PageInfo.SortReverse ? model.MailAddressesList.OrderByDescending(key => key.City) : model.MailAddressesList.OrderBy(key => key.City);
                    break;
                case "Street":
                    model.MailAddressesList = model.PageInfo.SortReverse ? model.MailAddressesList.OrderByDescending(key => key.Street) : model.MailAddressesList.OrderBy(key => key.Street);
                    break;
                case "Index":
                    model.MailAddressesList = model.PageInfo.SortReverse ? model.MailAddressesList.OrderByDescending(key => key.Index) : model.MailAddressesList.OrderBy(key => key.Index);
                    break;
                case "HouseNumber":
                    model.MailAddressesList = model.PageInfo.SortReverse ? model.MailAddressesList.OrderByDescending(key => key.HouseNumber) : model.MailAddressesList.OrderBy(key => key.HouseNumber);
                    break;
                case "CreationDate":
                    model.MailAddressesList = model.PageInfo.SortReverse ? model.MailAddressesList.OrderByDescending(key => key.CreationDate) : model.MailAddressesList.OrderBy(key => key.CreationDate);
                    break;
            }
            //update page info
            model.PageInfo.TotalItems = model.MailAddressesList.Count();
            // make pagination
            model.MailAddressesList = model.MailAddressesList.Skip((model.PageInfo.PageNumber - 1) * model.PageInfo.PageSize).Take(model.PageInfo.PageSize);
            // returns result model
            return new JsonResult { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Initialize page with start page data
        /// </summary>
        /// <returns>data for setup page filter boundaries</returns>
        [HttpGet]
        public JsonResult Init()
        {
            var filterData = new
            {
                mailAdsTableService.Filter.MinHouseNumberFilter,
                mailAdsTableService.Filter.MaxHouseNumberFilter,
                mailAdsTableService.Filter.MaxCreationDate,
                mailAdsTableService.Filter.MinCreationDate,
                count = mailAdsTableService.GetMailAddresses().Count()
            };
            return new JsonResult { Data = filterData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
