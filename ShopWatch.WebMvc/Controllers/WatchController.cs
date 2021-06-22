using ShopWatch.BussinessLogicLayer;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.Model;
using ShopWatch.Model.DataContext;
using ShopWatch.WebMvc.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopWatch.WebMvc.Controllers
{
	public class WatchController : Controller
	{
		private readonly IWatchService _watchServices;
		private readonly ShopWatchDataContext _context;
		public WatchController(IWatchService watchServices,ShopWatchDataContext context)
		{
			_watchServices = watchServices;
			_context = context;
		}
		public ActionResult ListByCategory(string category)
		{
			IEnumerable<Watch> watches;
			string currentCategory = string.Empty;

			if (category.IsBlank())
			{
				watches = _watchServices.GetAsync(orderBy: b => b.OrderBy(x => x.WatchName), page: 1, pageSize: 10);
				currentCategory = "All of Watches";
			}
			else
			{
				watches = _watchServices.GetAsync(filter: b => b.Category.CategoryName == category, orderBy: b => b.OrderBy(x => x.WatchName), page: 1, pageSize: 10);
				currentCategory = category;
			}

			return View(new WatchListByCategoryViewModel
			{
				Watches = watches,
				CurrentCategory = currentCategory
			});
		}

		public ActionResult ListByPublisher(string publisher)
		{
			IEnumerable<Watch> watches;
			string currentPublisher = string.Empty;

			if (publisher.IsBlank())
			{
				watches = _watchServices.GetAsync(orderBy: b => b.OrderBy(x => x.WatchName), page: 1, pageSize: 100).Where(s=>s.IsActive==true);
				currentPublisher = "All of Watches";
			}
			else
			{
				watches = _watchServices.GetAsync(filter: b => b.Publisher.PublisherName == publisher, orderBy: b => b.OrderBy(x => x.WatchName), page: 1, pageSize: 100).Where(s => s.IsActive == true);
				currentPublisher = publisher;
			}

			return View(new WatchListByPublisherViewModel
			{
				Watches = watches,
				CurrentPublisher = currentPublisher
			});
		}
		public  ActionResult ListByPrice(decimal priceLevel)
		{
			IEnumerable<Watch> watches;
			decimal currentPrice = decimal.Zero;

			if (priceLevel < 0)
			{
				watches = _watchServices.GetAsync(orderBy: b => b.OrderBy(x => x.Price), page: 1, pageSize: 100);
				currentPrice = 0;
			}
			else if(priceLevel<=1000000)
			{
				watches = _watchServices.GetAsync(filter: b => b.Price < priceLevel, orderBy: b => b.OrderBy(x => x.Price), page: 1, pageSize: 100);

				currentPrice = priceLevel;
			}
			else if (priceLevel > 1000000 && priceLevel <=5000000)
			{
				watches = _watchServices.GetAsync(filter: b => (b.Price < priceLevel && b.Price>1000000), orderBy: b => b.OrderBy(x => x.Price), page: 1, pageSize: 100);

				currentPrice = priceLevel;
			}
			else if (priceLevel > 5000000 && priceLevel <= 10000000)
			{
				watches = _watchServices.GetAsync(filter: b => (b.Price < priceLevel && b.Price > 5000000), orderBy: b => b.OrderBy(x => x.Price), page: 1, pageSize: 100);

				currentPrice = priceLevel;
			}
			else if (priceLevel > 10000000 && priceLevel <= 20000000)
			{
				watches = _watchServices.GetAsync(filter: b => (b.Price < priceLevel && b.Price > 10000000), orderBy: b => b.OrderBy(x => x.Price), page: 1, pageSize: 100);

				currentPrice = priceLevel;
			}
			else
			{
				watches = _watchServices.GetAsync(filter: b => (b.Price <= priceLevel && b.Price>20000000), orderBy: b => b.OrderBy(x => x.Price), page: 1, pageSize: 100);

				currentPrice = priceLevel;
			}	
			return View(new WatchListByPriceViewModel
			{
				Watches = watches.Where(s => s.IsActive == true),
				CurrentPrice = currentPrice
			});
		}
		public ActionResult Details(int id)
		{
			var watch =  _watchServices.GetById(id);
			if (watch == null)
				return HttpNotFound();
			return View(watch);
		}
		public ActionResult ListByPromotion()
		{
			var watch = _watchServices.GetAll().Where(s => s.IsActive == true);
			var listwatch = watch.Where(s => s.Promotions.
			/*Where(a => (a.StartDate <= DateTime.Now && a.EndDate >= DateTime.Now))*/Count() > 0);
			return View(listwatch);
		}
		public ActionResult ListByTop()
		{
			var result = _context.Watches.Where(s => (s.OrderDetails.Count!=0 && s.IsActive==true)).Take(10);
			return View(result);
		}
	}
}