using ShopWatch.Model;
using ShopWatch.Model.DataContext;
using ShopWatch.Model.Enum;
using ShopWatch.WebMvc.Areas.Admin.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopWatch.WebMvc.Areas.Admin.Controllers
{
	public class StatisticalManagementController : Controller
	{
		private readonly ShopWatchDataContext _context;
		public StatisticalManagementController(ShopWatchDataContext context)
		{
			_context = context;
		}
		// GET: Admin/Statistical
		public ActionResult Index()
		{
			var result = _context.Orders.Where(s => (s.Status == Status.Finish));
			ViewBag.year = _context.Orders.Select(s => s.CreatedDate.Year).Distinct().ToList();
			return View(result);
		}
		public ActionResult ByMonth(int? nam)
		{
			if (nam == null) nam = 2021;
			var result = _context.Orders.Where(s => (s.Status == Status.Finish && s.CreatedDate.Year==nam));
			ViewBag.year =_context.Orders.Select(s => s.CreatedDate.Year).Distinct().ToList();

			//ViewBag.year = new SelectList(_context.Orders.Select(s => s.CreatedDate.Year).Distinct().ToList());
			return View(result);
		}
		
		public ActionResult ByDay(int? year,int? month)
		{
			if (year == null) year = 2021;
			if (month == null) month = 1;
			ViewBag.year = _context.Orders.Select(s => s.CreatedDate.Year).Distinct().ToList();
			var result = _context.Orders.Where(s => (s.Status == Status.Finish && s.CreatedDate.Year == year && s.CreatedDate.Month==month));
			return View(result);
		}

		
	}
}