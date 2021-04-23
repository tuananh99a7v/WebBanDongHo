using ShopWatch.BussinessLogicLayer;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.Model;
using ShopWatch.WebMvc.ViewModels.Customer;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopWatch.WebMvc.Controllers
{
	public class WatchController : Controller
	{
		private readonly IWatchService _watchServices;
		public WatchController(IWatchService watchServices)
		{
			_watchServices = watchServices;
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
				watches = _watchServices.GetAsync(orderBy: b => b.OrderBy(x => x.WatchName), page: 1, pageSize: 100);
				currentPublisher = "All of Watches";
			}
			else
			{
				watches = _watchServices.GetAsync(filter: b => b.Publisher.PublisherName == publisher, orderBy: b => b.OrderBy(x => x.WatchName), page: 1, pageSize: 100);
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
				watches = _watchServices.GetAsync(orderBy: b => b.OrderBy(x => x.WatchName), page: 1, pageSize: 100);
				currentPrice = 0;
			}
			else
			{
				watches = _watchServices.GetAsync(filter: b => b.Price < priceLevel, orderBy: b => b.OrderBy(x => x.WatchName), page: 1, pageSize: 100);

				currentPrice = priceLevel;
			}

			return View(new WatchListByPriceViewModel
			{
				Watches = watches,
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
	}
}