using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.Model.DataContext;
using ShopWatch.WebMvc.Models;
using System.Linq;
using System.Web.Mvc;

namespace ShopWatch.WebMvc.Controllers
{
	public class PurchaseOrderController : Controller
    {
        private readonly ShopWatchDataContext _context;
        private readonly IOrderService _orderService;
        public PurchaseOrderController(ShopWatchDataContext context,IOrderService orderService)
		{
            _context = context;
            _orderService = orderService;
		}
        public ActionResult Index()
        {
			var session = (UserLogin)Session["UserSession"];
            var user = _context.Users.SingleOrDefault(m => m.AccountId == session.AccountId);
            var listOrder = _context.Orders.Where(m => m.UserId == user.UserId).ToList();
            return View(listOrder);
        }
        public ActionResult Detail(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null) return HttpNotFound();
            return View(order);
        }
        public ActionResult Cancelled(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null) return HttpNotFound();
            order.Status = Model.Enum.Status.Cancelled;
            _orderService.Update(order);
			return RedirectToAction("Index");
        }
    }
}