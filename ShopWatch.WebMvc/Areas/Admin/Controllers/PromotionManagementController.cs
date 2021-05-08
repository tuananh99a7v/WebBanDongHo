using AutoMapper;
using ShopWatch.BussinessLogicLayer;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.Model;
using ShopWatch.Model.DataContext;
using ShopWatch.WebMvc.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace ShopWatch.WebMvc.Areas.Admin.Controllers
{
	public class PromotionManagementController : Controller
    {
        private readonly IPromotionService _promotionService;
        private readonly ShopWatchDataContext _context = new ShopWatchDataContext();
        public PromotionManagementController(IPromotionService PromotionService)
        {
            _promotionService = PromotionService;
        }
        public ActionResult Index(string sortOrder, string currentFilter,
           string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder.IsBlank() ? "name_desc" : "";
            ViewData["StartDateSortParm"] = sortOrder == "Start" ? "start_desc" : "Start";
            ViewData["EndDateSortParm"] = sortOrder == "End" ? "end_desc" : "End";
            ViewData["PercentSortParm"] = sortOrder == "Percent" ? "percent_desc" : "Percent";
            ViewData["TotalSortParm"] = sortOrder == "Total" ? "total_desc" : "Total";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Promotion, bool>> filter = null;
            Func<IQueryable<Promotion>, IOrderedQueryable<Promotion>> orderBy = null;

            if (searchString.IsNotBlank())
            {
                filter = a => a.PromotionName.Contains(searchString);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = b => b.OrderByDescending(s => s.PromotionName);
                    break;
                case "Start":
                    orderBy = b => b.OrderBy(s => s.StartDate);
                    break;
                case "start_desc":
                    orderBy = b => b.OrderByDescending(s => s.StartDate);
                    break;
                case "End":
                    orderBy = b => b.OrderByDescending(s => s.EndDate);
                    break;
                case "end_desc":
                    orderBy = b => b.OrderByDescending(s => s.EndDate);
                    break;
                case "Percent":
                    orderBy = b => b.OrderByDescending(s => s.Percent);
                    break;
                case "percent_desc":
                    orderBy = b => b.OrderByDescending(s => s.Percent);
                    break;
                case "Total":
                    orderBy = b => b.OrderBy(s => s.Watches.Count());
                    break;
                case "total_desc":
                    orderBy = b => b.OrderByDescending(s => s.Watches.Count());
                    break;
                default:
                    orderBy = b => b.OrderBy(s => s.PromotionName);
                    break;
            }

            var promotions = _promotionService.GetAsync(filter: filter, orderBy: orderBy, page: page ?? 1, pageSize: 10);

            return View(promotions);
        }

        public ActionResult AddPromotion()
        {
            var promotionEditViewModel = new PromotionEditViewModel();
            return View(promotionEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
		public ActionResult AddPromotion(PromotionEditViewModel promotionEditViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(promotionEditViewModel);
			}

			var promotion = Mapper.Map<Promotion>(promotionEditViewModel);

			_promotionService.Create(promotion);

			return RedirectToAction("Index");

		}
		public ActionResult EditPromotion(int id)
        {
            var promotion = _promotionService.GetById(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            var PromotionEditViewModel = Mapper.Map<PromotionEditViewModel>(promotion);

            return View(PromotionEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditPromotion(PromotionEditViewModel promotionEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(promotionEditViewModel);
            }

            var promotion = Mapper.Map<Promotion>(promotionEditViewModel);
            _promotionService.Update(promotion);
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var user = _promotionService.GetById(id);

            var promotionViewModel = Mapper.Map<PromotionViewModel>(user);
            return View(promotionViewModel);
        }
        [HttpPost]
        public ActionResult DeletePromotion(int id)
        {
   //         var promotion = _promotionService.GetById(id);
			//foreach (var item in promotion.Watches)
			//{
   //             promotion.
			//}
            _promotionService.Delete(id);
            return RedirectToAction("Index");
        }
        [AcceptVerbs("Get", "Post")]
        public ActionResult CheckIfCategoryNameAlreadyExists([Bind(Prefix = "Pro.PromotionName")] string name)
        {
            var category = _promotionService.Find(b => b.PromotionName == name);
            return category == null ? Json(true) : Json("Category name exists");
        }
    }
}