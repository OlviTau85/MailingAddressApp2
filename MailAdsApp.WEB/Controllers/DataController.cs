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

        public DataController(IMailAdsTableService serv)
        {
            mailAdsTableService = serv;
        }

        //
        // GET: /Data/

        public JsonResult Index()
        {
            IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
            var mailAddresses = Mapper.Map<IEnumerable<MailAddressDTO>, List<MailAddressViewModel>>(mailAdsTableService.GetMailAddresses()).Take(10);

            return new JsonResult { Data = mailAddresses, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}
