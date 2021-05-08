using ShopWatch.Model.DataContext;
using ShopWatch.WebMvc.Areas.Admin.ViewModel;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace ShopWatch.WebMvc.Areas.Admin.Controllers
{
	public class DashboardController : Controller
    {
        private readonly ShopWatchDataContext _context;
        public DashboardController(ShopWatchDataContext context)
		{
            _context = context;
		}
        public ActionResult Index()
        {
            #region Cập nhật giá khuyến mãi của sản phẩm khi không có khuyến mãi nào đang được áp dụng
            var watches = _context.Watches.ToList();
            foreach (var item in watches)
            {
                if (item.Promotions.Count() == 0)
                {
                    item.PricePromotion = item.Price;
                }
                foreach (var i in item.Promotions)
                {
                    if (i.EndDate < DateTime.Now)
                    {
                        item.PricePromotion = item.Price;
                    }
                }
                _context.Watches.AddOrUpdate(item);
                _context.SaveChanges();
            }
            #endregion

            var dashboardViewModel = new DashboardViewModel()
            {
                OrderWating = _context.Orders.Where(s => s.Status == Model.Enum.Status.WaitingDelivery),
                WatchesEnd=_context.Watches.Where(s=>s.Quantity<10),
                WatchesOrder=_context.Watches.OrderByDescending(s=>s.OrderDetails.Sum(o=>(o.Quantity*o.UnitPrice))).Take(3),
            };
            dashboardViewModel.TotalOrderWaiting = dashboardViewModel.OrderWating.Count();
            dashboardViewModel.TotalWatchesEnd = dashboardViewModel.WatchesEnd.Count();
			var result = _context.Orders/*.Where(o => o.CreatedDate.Equals(DateTime.Now))*/;
            if (result == null)
            {
                dashboardViewModel.TotalPriceOrder = 0;
            }
            else
            {
                dashboardViewModel.TotalPriceOrder = result.Sum(s => s.OrderDetails.Sum(a => (a.Quantity * a.UnitPrice)));

            }
			
			return View(dashboardViewModel);
        }
    }
}