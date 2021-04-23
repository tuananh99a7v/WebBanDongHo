using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopWatch.WebMvc.Areas.Admin.Controllers
{
    public class HomeManagementController : Controller
    {
        // GET: Admin/HomeManagement
        public ActionResult Index()
        {
            return View();
        }
    }
}