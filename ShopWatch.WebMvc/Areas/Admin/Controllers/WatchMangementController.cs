using AutoMapper;
using ShopWatch.BussinessLogicLayer;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.BussinessLogicLayer.Services;
using ShopWatch.Model;
using ShopWatch.Model.DataContext;
using ShopWatch.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
namespace ShopWatch.WebMvc.Areas.Admin.Controllers
{
	public class WatchMangementController : Controller
    {
        private readonly IWatchService _watchService;
        private readonly ShopWatchDataContext _context = new ShopWatchDataContext();
        private readonly ICategoryService _categoryService;
        private readonly IPublisherService _publisherService;

        public WatchMangementController(IWatchService watchService, ICategoryService categoryService, IPublisherService publisherService)
        {
            _watchService = watchService;
            _categoryService = categoryService;
            _publisherService = publisherService;
            
        }
        public ActionResult Index(string sortOrder, string currentFilter,
           string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder.IsBlank() ? "name_desc" : "";
            ViewData["QuantitySortParm"] = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["PricePromotionSortParm"] = sortOrder == "PricePromotion" ? "pricepromotiont_desc" : "PricePromotion";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Watch, bool>> filter = null;
            Func<IQueryable<Watch>, IOrderedQueryable<Watch>> orderBy = null;

            if (searchString.IsNotBlank())
            {
                filter = a => a.WatchName.Contains(searchString);
            }
            switch (sortOrder)
            {
                case "":
                    orderBy = b => b.OrderBy(s => s.WatchName);
                    break;
                case "name_desc":
                    orderBy = b => b.OrderByDescending(s => s.WatchName);
                    break;
                case "Quantity":
                    orderBy = b => b.OrderBy(s => s.Quantity);
                    break;
                case "quantity_desc":
                    orderBy = b => b.OrderByDescending(s => s.Quantity);
                    break;
                case "Price":
                    orderBy = b => b.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    orderBy = b => b.OrderByDescending(s => s.Price);
                    break;
                case "PricePromotion":
                    orderBy = b => b.OrderBy(s => s.PricePromotion);
                    break;
                case "pricepromotion_desc":
                    orderBy = b => b.OrderByDescending(s => s.PricePromotion);
                    break;
                
                default:
                    orderBy = b => b.OrderBy(s => s.Quantity);
                    break;
            }

            var Watchs = _watchService.GetAsync(filter: filter, orderBy: orderBy, page: page ?? 1, pageSize: 10);
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
                    if (i.EndDate < DateTime.Now || i.StartDate>DateTime.Now)
                    {
                        item.PricePromotion = item.Price;
                    }
                }
                _context.Watches.AddOrUpdate(item);
                _context.SaveChanges();
            }
            #endregion

            return View(Watchs);
        }
        public ActionResult Details(int id)
        {
            var user = _watchService.GetById(id);
            var watchViewModel = Mapper.Map<WatchViewModel>(user);
            return View(watchViewModel);
        }
        public ActionResult AddWatch()
        {
            var watchEditViewModel = new WatchEditViewModel();
            
            List<Category> categories =  _categoryService.GetAsync(orderBy: x => x.OrderBy(b => b.CategoryName), page: 1, pageSize: 10);
            ViewBag.Categories = new SelectList(categories.OrderBy(x => x.CategoryId), "CategoryId", "CategoryName");
            List<Publisher> publishers =  _publisherService.GetAsync(orderBy: x => x.OrderBy(b => b.PublisherName), page: 1, pageSize: 10);
            ViewBag.Publishers = new SelectList(publishers.OrderBy(x => x.PublisherId), "PublisherId", "PublisherName");
            return View(watchEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddWatch(WatchEditViewModel watchEditViewModel,HttpPostedFileBase Img,
            HttpPostedFileBase small1, HttpPostedFileBase small2, HttpPostedFileBase small3)
        {
            if (!ModelState.IsValid)
            {
                var watch = Mapper.Map<Watch>(watchEditViewModel);
                string fileName = "";
				int id = _context.Watches.ToList().Last().WatchId + 1;
                watch.SmallImage = "SP"+id;
                if (Img != null && Img.ContentLength > 0)
                {
                    try
                    {
                        int index = Img.FileName.IndexOf(".");
                        fileName = "SP000" + id.ToString() + "." + Img.FileName.Substring(index + 1);
                        string path = Path.Combine(Server.MapPath("~/Assets/images/HINHLON"), fileName);
                        Img.SaveAs(path);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                if (small1 != null && small1.ContentLength > 0)
                {

					try
					{
						string path = Path.Combine(Server.MapPath("~/Assets/images/HINHNHO/" + watch.SmallImage), "1.jpg");
						small1.SaveAs(path);
					}
					catch (Exception)
					{

						throw;
					}
                }
                if (small3 != null && small3.ContentLength > 0)
                {

					try
					{
						string path = Path.Combine(Server.MapPath("~/Assets/images/HINHNHO/" + watch.SmallImage), "3.jpg");
						small3.SaveAs(path);
					}
					catch (Exception)
					{

						throw;
					}


                }
                if (small2 != null && small2.ContentLength > 0)
                {

					try
					{
						string path = Path.Combine(Server.MapPath("~/Assets/images/HINHNHO/" + watch.SmallImage), "2.jpg");
						small2.SaveAs(path);
					}
					catch (Exception)
					{

						throw;
					}
                }

                watch.CreatedDate = DateTime.Now;
                watch.ModifiedDate = DateTime.Now;
                watch.ImageUrl = fileName;
                watch.PricePromotion = watchEditViewModel.Price;
                _watchService.Create(watch);

                return RedirectToAction("Index");
            }
            return View(watchEditViewModel);
        }
        public ActionResult EditWatch(int id)
        {
            List<Category> categories = _categoryService.GetAsync(orderBy: x => x.OrderBy(b => b.CategoryName), page: 1, pageSize: 100);
            ViewBag.Categories = new SelectList(categories.OrderBy(x => x.CategoryId), "CategoryId", "CategoryName");
            List<Publisher> publishers = _publisherService.GetAsync(orderBy: x => x.OrderBy(b => b.PublisherName), page: 1, pageSize: 100);
            ViewBag.Publishers = new SelectList(publishers.OrderBy(x => x.PublisherId), "PublisherId", "PublisherName");
            var watch = _watchService.GetById(id);
            if (watch == null)
            {
                return HttpNotFound();
            }
            

            return View(watch);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditWatch(Watch watche,HttpPostedFileBase Img,
            HttpPostedFileBase small1, HttpPostedFileBase small2, HttpPostedFileBase small3)
        {
           
            if (!ModelState.IsValid)
            {
                var watch = _watchService.GetById(watche.WatchId);
                string fileName = "";

                int id = _context.Watches.ToList().Last().WatchId + 1;
                if (Img != null && Img.ContentLength > 0)
                {
                    try
                    {
                        int index = Img.FileName.IndexOf(".");
                        fileName = "SP000" + id.ToString() + "." + Img.FileName.Substring(index + 1);
                        string path = Path.Combine(Server.MapPath("~/Assets/images/HINHLON"), fileName);
                        Img.SaveAs(path);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                if (small1 != null && small1.ContentLength > 0)
                {

                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/Assets/images/HINHNHO/" + watch.SmallImage), "1.jpg");
                        small1.SaveAs(path);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                if (small3 != null && small3.ContentLength > 0)
                {

                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/Assets/images/HINHNHO" + watch.SmallImage), "3.jpg");
                        small3.SaveAs(path);
                    }
                    catch (Exception )
                    {
                        throw ;
                    }


                }
                if (small2 != null && small2.ContentLength > 0)
                {

                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/Assets/images/HINHNHO" + watch.SmallImage), "2.jpg");
                        small2.SaveAs(path);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                watch.ModifiedDate = DateTime.Now;
                if(fileName!="") watch.ImageUrl = fileName;
                watch.Description = watche.Description;
                watch.Evaluate = watche.Evaluate;
                watch.Price = watche.Price;
                watch.WatchName = watche.WatchName;
                watch.CategoryId = watche.CategoryId;
                watch.PublisherId = watche.PublisherId;
                watch.PricePromotion = watch.Price;
                watch.Quantity = watche.Quantity;
                _watchService.Update(watch);
            }
            return RedirectToAction("Index");
        }
    
        
        [HttpPost]
        public ActionResult DeleteWatch(int id)
        {
            _watchService.Delete(id);
            return RedirectToAction("Index");
        }
        [AcceptVerbs("Get", "Post")]
        public ActionResult CheckIfWatchNameAlreadyExists([Bind(Prefix = "Pro.WatchName")] string name)
        {
            var category = _watchService.Find(b => b.WatchName == name);
            return category == null ? Json(true) : Json("Category name exists");
        }
    }
}