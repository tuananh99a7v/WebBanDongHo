using AutoMapper;
using ShopWatch.BussinessLogicLayer;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.Model;
using ShopWatch.Model.DataContext;
using ShopWatch.WebMvc.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ShopWatch.WebMvc.Areas.Admin.Controllers
{
	public class PublisherManagementController : Controller
    {
        private readonly IPublisherService _publisherService;
        private readonly ShopWatchDataContext _context = new ShopWatchDataContext();
        public PublisherManagementController(IPublisherService publisherService)
		{
            _publisherService = publisherService;
		}
        public ActionResult Index(string sortOrder, string currentFilter,
           string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder.IsBlank() ? "name_desc" : "";
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

            Expression<Func<Publisher, bool>> filter = null;
            Func<IQueryable<Publisher>, IOrderedQueryable<Publisher>> orderBy = null;

            if (searchString.IsNotBlank())
            {
                filter = a => a.PublisherName.Contains(searchString);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = b => b.OrderByDescending(s => s.PublisherName);
                    break;
                case "Total":
                    orderBy = b => b.OrderBy(s => s.Watches.Count());
                    break;
                case "total_desc":
                    orderBy = b => b.OrderByDescending(s => s.Watches.Count());
                    break;
                default:
                    orderBy = b => b.OrderBy(s => s.PublisherName);
                    break;
            }

            var publishers = _publisherService.GetAsync(filter: filter, orderBy: orderBy, page: page ?? 1, pageSize: 10);

            return View(publishers);
        }

        public ActionResult AddPublisher()
        {
            var publisherEditViewModel = new PublisherEditViewModel();
            return View(publisherEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddPublisher(PublisherEditViewModel publisherEditViewModel,HttpPostedFileBase ImgUrl)
        {
            if (!ModelState.IsValid)
            {
                string fileName = "";
                if (ImgUrl != null && ImgUrl.ContentLength > 0)
                {
                    try
                    {
                        int id = _context.Publishers.ToList().Last().PublisherId + 1;
                        int index = ImgUrl.FileName.IndexOf(".");
                        fileName = "p" + id.ToString() + "." + ImgUrl.FileName.Substring(index + 1);
                        string path = Path.Combine(Server.MapPath("~/Assets/images/HINHTH"), fileName);
                        ImgUrl.SaveAs(path);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                var publisher = Mapper.Map<Publisher>(publisherEditViewModel);
                publisher.Image = fileName;
                _publisherService.Create(publisher);

                return RedirectToAction("Index");
            }
            return View(publisherEditViewModel);

        }
        public ActionResult EditPublisher(int id)
        {
            var publisher = _publisherService.GetById(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            var publisherEditViewModel = Mapper.Map<PublisherEditViewModel>(publisher);

            return View(publisherEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditPublisher(PublisherEditViewModel publisherEditViewModel, HttpPostedFileBase ImgUrl)
        {
            if (!ModelState.IsValid)
            {
                var publisherEdit = Mapper.Map<Publisher>(publisherEditViewModel);
                var publisher = _publisherService.GetById(publisherEdit.PublisherId);
                if (ImgUrl != null && ImgUrl.ContentLength > 0)
                {
                    string filePath = Path.Combine(Server.MapPath("~/Assets/images/HINHTH"), Path.GetFileName(ImgUrl.FileName));
                    ImgUrl.SaveAs(filePath);
                    publisherEditViewModel.Image = ImgUrl.FileName;
                }
                if(publisherEdit.Image!="") publisher.Image = publisherEditViewModel.Image;
                publisher.PublisherName = publisherEdit.PublisherName;
                publisher.Description = publisherEdit.Description;
                _publisherService.Update(publisher);
                return RedirectToAction("Index");
            }

            return View(publisherEditViewModel);
        }

        [HttpPost]
        public ActionResult DeletePublisher(int id)
        {
            _publisherService.Delete(id);
            return RedirectToAction("Index");
        }
        [AcceptVerbs("Get", "Post")]
        public ActionResult CheckIfCategoryNameAlreadyExists([Bind(Prefix = "Category.CategoryName")] string name)
        {
            var category = _publisherService.Find(b => b.PublisherName == name);
            return category == null ? Json(true) : Json("Category name exists");
        }
    }
}