using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.Model;
using ShopWatch.Model.DataContext;
using ShopWatch.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopWatch.WebMvc.Areas.Admin.Controllers
{
	public class WatchPromotionManagementController : Controller
    {
        private readonly IPromotionService _promotionService;
        private readonly IWatchService _watchService;
        private readonly ShopWatchDataContext _context;

        public WatchPromotionManagementController(IPromotionService promotionService,IWatchService watchService,ShopWatchDataContext context)
		{
            _promotionService = promotionService;
            _watchService = watchService;
            _context = context;
		}
        public ActionResult Index()
        {
            var listPromotion = _promotionService.GetAll();
            return View(listPromotion);
        }
        public ActionResult AddWatchPromotion()
        {
            var haiz = new WatchPromotionViewModel();
            List<Promotion> promotions = _promotionService.GetAll().ToList();
			foreach (var item in promotions)
			{
                if (item.EndDate < DateTime.Now) promotions.Remove(item); 
			}
            ViewBag.promotions = new SelectList(promotions.OrderBy(x => x.Percent), "PromotionId", "PromotionName");
            List<Watch> watches = _watchService.GetAll().ToList();
            ViewBag.watches = new SelectList(watches.OrderBy(x => x.WatchId), "WatchId", "WatchName");
            return View(haiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddWatchPromotion(WatchPromotionViewModel a)
        {
            if (!ModelState.IsValid)
            {
                return View(a);
            }
            var watchpromotion = _promotionService.GetById(a.PromotionId);
            var watch = _watchService.GetById(a.WatchId);
            watchpromotion.Watches.Add(watch);
            _promotionService.Update(watchpromotion);

            var ui = watch.Promotions.FirstOrDefault();
            if(watchpromotion.StartDate<=DateTime.Now && DateTime.Now<=watchpromotion.EndDate)
			{
                watch.PricePromotion = watch.Price - (watch.Price * ui.Percent) / 100;
            }
            _watchService.Update(watch);
            //string mn = "";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteWatchPromotion(int id,int id2)
        {
            var a = _watchService.GetById(id);
            var b = _promotionService.GetById(id2);
            b.Watches.Remove(a);
            _promotionService.Update(b);
            a.PricePromotion = a.Price;
            _watchService.Update(a);
            return RedirectToAction("Index");
        }
    }
}