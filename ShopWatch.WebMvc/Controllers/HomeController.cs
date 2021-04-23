using ShopWatch.BussinessLogicLayer;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.Model.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopWatch.WebMvc.Controllers
{
	public class HomeController : Controller
	{
		private IWatchService _watchServices;
		private ShopWatchDataContext _context = new ShopWatchDataContext();

		public HomeController(IWatchService watchServices)
		{
			_watchServices = watchServices;
		}
		public ActionResult Index(string search)
		{
		
			if (search==null || search=="")
			{
				var watches = _watchServices.GetAll().ToList();
				return View(watches);
			}
			else
			{
				var watches = _context.Watches.Where(s => s.WatchName.Contains(search)).ToList();
				return View(watches);
				
			}	
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}