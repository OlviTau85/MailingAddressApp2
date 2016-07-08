using MailAdsApp.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MailAdsApp.WEB.Controllers
{
    /// <summary>
    /// HomeController
    /// </summary>
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        /// <summary>
        /// Action Index
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

    }
}
