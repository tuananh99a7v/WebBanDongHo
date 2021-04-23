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
	public class UserManagementController : Controller
	{
		private readonly IUserService _userService;
		public UserManagementController(IUserService userService)
		{
			_userService = userService;
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

			Expression<Func<User, bool>> filter = null;
			Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null;

			if (searchString.IsNotBlank())
			{
				filter = a => a.Name.Contains(searchString);
			}
			switch (sortOrder)
			{
				case "name_desc":
					orderBy = b => b.OrderByDescending(s => s.Name);
					break;
				case "Total":
					//orderBy = b => b.OrderBy(s => s.Watches.Count());
					break;
				case "total_desc":
					//orderBy = b => b.OrderByDescending(s => s.Watches.Count());
					break;
				default:
					orderBy = b => b.OrderBy(s => s.Name);
					break;
			}

			var publishers = _userService.GetAsync(filter: filter, orderBy: orderBy, page: page ?? 1, pageSize: 10);
			return View(publishers);
		}
		public ActionResult Details(int id)
		{
			var user = _userService.GetById(id);

			var userViewModel = Mapper.Map<UserViewModel>(user);
			return View(userViewModel);
		}

		[AcceptVerbs("Get", "Post")]
		public ActionResult CheckIfCategoryNameAlreadyExists([Bind(Prefix = "User.Name")] string name)
		{
			var category = _userService.Find(b => b.Name == name);
			return category == null ? Json(true) : Json("Category name exists");
		}
	}
}