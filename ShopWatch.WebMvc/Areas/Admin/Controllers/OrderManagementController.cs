using ShopWatch.BussinessLogicLayer;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.Model;
using ShopWatch.Model.DataContext;
using ShopWatch.Model.Enum;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace ShopWatch.WebMvc.Areas.Admin.Controllers
{
	public class OrderManagementController : Controller
	{
		private readonly IOrderService _orderService;
		private readonly ShopWatchDataContext _context = new ShopWatchDataContext();
		public OrderManagementController(IOrderService orderService)
		{
			_orderService = orderService;
		}
		public ActionResult Index(string sortOrder, string currentFilter,
		   string searchString, int? page,Status? status)
		{

			ViewData["CurrentSort"] = sortOrder;
			ViewData["NameSortParm"] = sortOrder.IsBlank() ? "name_desc" : "";
			ViewData["QuantitySortParm"] = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
			ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
			ViewData["TotalSortParm"] = sortOrder == "Total" ? "pricepromotiont_desc" : "Total";

			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewData["CurrentFilter"] = searchString;

			Expression<Func<Order, bool>> filter = null;
			Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null;

			if (searchString.IsNotBlank())
			{
				filter = a => (a.User.Name.Contains(searchString) || a.ShipAddress.Contains(searchString));
			}
			else
			{
				if(status!= null)
				filter = a => a.Status == status;
			}	
			switch (sortOrder)
			{
				case "name_desc":
					orderBy = b => b.OrderByDescending(s => s.OrderId);
					break;
				case "Total":
					orderBy = b => b.OrderBy(s => s.OrderDetails.Sum(a => a.Quantity * a.UnitPrice));
					break;
				case "total_desc":
					orderBy = b => b.OrderByDescending(s => s.OrderDetails.Sum(a => a.Quantity * a.UnitPrice));
					break;
				default:
					orderBy = b => b.OrderBy(s => s.CreatedDate);
					break;
			}
			var orders = _orderService.GetAsync(filter: filter, orderBy: orderBy, page: page ?? 1, pageSize: 10);
			return View(orders);
		}


		// GET: Admin/OrderManagement/Details/5
		public ActionResult Details(int id)
		{
			var order = _orderService.GetById(id);
			if (order == null) return HttpNotFound();
			return View(order);
		}



		// GET: Admin/OrderManagement/Edit/5
		public ActionResult Edit(int id)
		{
			var a = _orderService.GetById(id);
			ViewBag.UserId = new SelectList(_context.Users, "UserId", "Name", a.UserId);

			return View(a);
		}

		// POST: Admin/OrderManagement/Edit/5
		[HttpPost]
		public ActionResult Edit(Order order)
		{
			if (ModelState.IsValid)
			{
				_orderService.Update(order);
				return RedirectToAction("Index");
			}
			ViewBag.UserId = new SelectList(_context.Users, "UserId", "Name", order.UserId);
			return View(order);
		}
		[HttpPost]
		public ActionResult Delete(int id)
		{
			var collection = _context.OrderDetails.Where(s => s.OrderId == id);
			foreach (var item in collection)
			{
				//Update watch quantity to Watch Model when watch has delete from oder
				var watch = _context.Watches.Find(item.WatchId);
				watch.Quantity += item.Quantity;

				//Delete earch orderdetail
				_context.OrderDetails.Remove(item);
			}
			_context.SaveChanges();
			var a = _context.Orders.Find(id);
			_context.Orders.Remove(a);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}


	}
}
