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
using PagedList;
using PagedList.Mvc;

namespace MailAdsApp.WEB.Controllers
{
    public class HomeController : Controller
    {
        IMailAdsTableService mailAdsTableService;
        public HomeController(IMailAdsTableService serv)
        {
            mailAdsTableService = serv;
        }

        //
        // GET: /Home/

        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
            IEnumerable<MailAddress> ads = Mapper.Map<IEnumerable<MailAddressDTO>, List<MailAddress>>(mailAdsTableService.GetMailAddresses());
            int pageNumber = (page ?? 1);
            
            return View(ads.ToList().ToPagedList(pageNumber, pageSize));
        }

    }
}
