using ShopWatch.BussinessLogicLayer;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.Model;
using ShopWatch.Model.DataContext;
using ShopWatch.WebMvc.Models;
using ShopWatch.WebMvc.ViewModels;
using ShopWatch.WebMvc.ViewModels.Customer;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopWatch.WebMvc.Controllers
{
	public class OrderController : Controller
	{
		private const string CartSession = "CartSession";
		private readonly IOrderService _orderService;
		private readonly ICheckoutService _checkoutService;
		private readonly ShopWatchDataContext _context = new ShopWatchDataContext();


		public OrderController(IOrderService orderService, ICheckoutService checkoutService)
		{
			_orderService = orderService;
			_checkoutService = checkoutService;
		}

		public ActionResult Checkout()
		{
			var session = (UserLogin)Session["UserSession"];
			if (session != null)
			{
				var user = _context.Users.SingleOrDefault(m => m.AccountId == session.AccountId);
				if(user==null)
				{
					HttpNotFound();
				}
				else
				{
					var orderViewModel = new OrderViewModel()
					{
						ShipAddress = user.Address+user.City,
						PhoneNumber = user.Phone,
					};
					return View(orderViewModel);
				}

			}
			return View();
		}

		[HttpPost]
		public ActionResult Checkout(OrderViewModel orderViewModel)
		{

			var cart = Session[ConstantCommon.Cart];
			var cartItems = (List<ShoppingCartItem>)cart;
			var session = (UserLogin)Session["UserSession"];
			var customer = _context.Users.SingleOrDefault(m => m.AccountId == session.AccountId);
			ViewBag.MessageUi = "";
			if (cartItems.Count == 0)
			{
				ModelState.AddModelError("", "Giỏ hàng của bạn hiện trống không ah ^_^, chọn mua đồng hồ đi nhé!");
			}

			if (ModelState.IsValid)
			{
				List<OrderDetail> orderDetails = new List<OrderDetail>();
				var order = new Order()
				{
					ShipAddress = orderViewModel.ShipAddress,
					PhoneNumber = orderViewModel.PhoneNumber,
					UserId = customer.UserId

				};
				foreach (var item in cartItems)
				{
					var orderDetail = new OrderDetail()
					{
						Watch = item.Watch,
						WatchId = item.WatchId,
						Quantity = item.Quantity,
						UnitPrice = item.Watch.PricePromotion,
						OrderId = order.OrderId
					};
					if (!CheckQuantity.Check(orderDetail.WatchId, orderDetail.Quantity))
					{
						ViewBag.MessageUi += "Không đủ số lượng yêu cầu";
						return View();
					}
					else
						orderDetails.Add(orderDetail);
				}
                _checkoutService.Checkout(order, orderDetails);
				cartItems.Clear();
				return RedirectToAction("CheckoutComplete");
			}
			return View(orderViewModel);
		}

		public ActionResult CheckoutComplete()
		{
			ViewBag.CheckoutCompleteMessage = " Cảm ơn bạn đã đặt hàng, đơn hàng của bạn sẽ sớm được chuyển tới trong vòng 7 ngày ahihi ^_^, nếu đặt nhầm xin vui lòng hủy ngay giúp cửa hàng Hùng Tâm, xin cảm ơn!";

			return View();
		}
	}
}