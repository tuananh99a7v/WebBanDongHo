using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.WebMvc.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ShopWatch.WebMvc.Controllers
{
	public class SearchController : Controller
	{
		private readonly ICategoryService _categoryServices;

		private readonly IPublisherService _publisherServices;

		public SearchController(
			ICategoryService categoryServices,

			IPublisherService publisherServices)
		{
			_categoryServices = categoryServices;

			_publisherServices = publisherServices;
		}

		public ActionResult CategorySearch()
		{
			var syncContext = SynchronizationContext.Current;
			SynchronizationContext.SetSynchronizationContext(null);
			var categories = _categoryServices.GetAsync(orderBy: c => c.OrderBy(x => x.CategoryName), page: 1, pageSize: 3);
			SynchronizationContext.SetSynchronizationContext(syncContext);

			return PartialView(categories);
		}



		public ActionResult PublisherSearch()
		{
			var syncContext = SynchronizationContext.Current;
			SynchronizationContext.SetSynchronizationContext(null);
			var publishers = _publisherServices.GetAsync(orderBy: p => p.OrderBy(x => x.PublisherName));
			SynchronizationContext.SetSynchronizationContext(syncContext);
			return PartialView(publishers);
		}

		public ActionResult PriceSearch()
		{
			var priceSearchItems = new List<PriceSearchItem>
			{
				new PriceSearchItem()
				{
					DisplayValue =1000000,
					ActionValue = 1000000,

				},
				new PriceSearchItem()
				{
					DisplayValue = 5000000,
					ActionValue = 5000000,

				},
				new PriceSearchItem()
				{
					DisplayValue = 10000000,
					ActionValue = 10000000,

				},
				new PriceSearchItem()
				{
					DisplayValue = 20000000,
					ActionValue = 20000000,

				}
			};

			return PartialView(priceSearchItems);
		}
	}

}
