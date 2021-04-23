using ShopWatch.BussinessLogicLayer;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.Model;
using ShopWatch.WebMvc.ViewModels.Customer;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopWatch.WebMvc.Controllers
{
	public class ShoppingCartController : Controller
    {
        private readonly IWatchService _watchServices;
        public ShoppingCartController(IWatchService watchServices)
        {
            _watchServices = watchServices;
        }
        public ActionResult Index()
        {
            var cart = Session[ConstantCommon.Cart] as List<ShoppingCartItem> ?? new List<ShoppingCartItem>();

            if (cart.Count == 0 || Session[ConstantCommon.Cart] == null)
            {
                ViewBag.Message = "Don't have any item in your cart.";
                return View();
            }

            decimal total = 0m;
            foreach (var item in cart)
            {
                total += item.Total;
            }
            ViewBag.GrandTotal = total;

            return View(cart);
        }

        public ActionResult CartPartial()
        {
            ShoppingCartItem model = new ShoppingCartItem();

            int qty = 0;
            decimal price = 0m;
            if (Session[ConstantCommon.Cart] != null)
            {
                var list = (List<ShoppingCartItem>)Session[ConstantCommon.Cart];
                foreach (var item in list)
                {
                    qty += item.Quantity;
                    price += item.Quantity * item.Price;
                }
                model.Quantity = qty;
                model.Price = price;
            }
            else
            {
                model.Quantity = 0;
                model.Price = 0m;
            }

            return PartialView(model);
        }

        public ActionResult AddToCartPartial(int id)
        {
            List<ShoppingCartItem> cart = Session[ConstantCommon.Cart] as List<ShoppingCartItem> ?? new List<ShoppingCartItem>();

            ShoppingCartItem model = new ShoppingCartItem();

            Watch watch = _watchServices.GetById(id);

            var itemInCart = cart.FirstOrDefault(x => x.WatchId == id);

            if (itemInCart == null)
            {
                cart.Add(new ShoppingCartItem()
                {
                    Watch = watch,
                    WatchId = watch.WatchId,
                    Quantity = 1,
                    Price = watch.PricePromotion,
                });
            }
            else
            {
                itemInCart.Quantity++;
            }

            int qty = 0;
            decimal price = 0m;
            foreach (var item in cart)
            {
                qty += item.Quantity;
                price += item.Quantity * item.Price;
            }
            model.Quantity = qty;
            model.Price = price;
            Session[ConstantCommon.Cart] = cart;

            return PartialView(model);
        }

        public JsonResult IncrementProduct(int watchId)
        {
            List<ShoppingCartItem> cart = Session[ConstantCommon.Cart] as List<ShoppingCartItem>;

            ShoppingCartItem item = cart.FirstOrDefault(x => x.WatchId == watchId);

            item.Quantity++;

            var result = new { qty = item.Quantity, price = item.Price };

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DecrementProduct(int watchId)
        {
            List<ShoppingCartItem> cart = Session[ConstantCommon.Cart] as List<ShoppingCartItem>;

            ShoppingCartItem item = cart.FirstOrDefault(x => x.WatchId == watchId);

            if (item.Quantity > 1)
            {
                item.Quantity--;
            }
            else
            {
                item.Quantity = 0;
                cart.Remove(item);
            }

            var result = new { qty = item.Quantity, price = item.Price };

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public void RemoveProduct(int watchId)
        {
            List<ShoppingCartItem> cart = Session[ConstantCommon.Cart] as List<ShoppingCartItem>;

            ShoppingCartItem item = cart.FirstOrDefault(x => x.WatchId == watchId);

            cart.Remove(item);

        }
    }
}