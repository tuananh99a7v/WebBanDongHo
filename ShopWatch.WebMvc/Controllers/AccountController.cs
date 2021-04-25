using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.BussinessLogicLayer.Services;
using ShopWatch.Model;
using ShopWatch.Model.DataContext;
using ShopWatch.WebMvc.Models;
using ShopWatch.WebMvc.ViewModels;
using ShopWatch.WebMvc.ViewModels.Customer;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace ShopWatch.WebMvc.Controllers
{
	public class AccountController : Controller
	{

		private readonly IAccountService _accountService;
		private readonly ShopWatchDataContext _context;

		public AccountController(IAccountService accountService, ShopWatchDataContext context)
		{
			_accountService = accountService;
			_context = context;
		}

		[HttpGet]
		public ActionResult Login()
		{
			LoginViewModel loginViewModel = new LoginViewModel();
			ViewBag.MessageLogin = "";
			return View(loginViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateInput(false)]
		public ActionResult Login(LoginViewModel loginViewModel)
		{
			ViewBag.MessageLogin = "";
			if (ModelState.IsValid)
			{
				LoginService loginService = new LoginService();
				var result = loginService.Login(loginViewModel.AccountName, loginViewModel.PassWord);
				if (result == 1)
				{
					var user = loginService.GetById(loginViewModel.AccountName);
					var userSession = new UserLogin();
					userSession.AccountName = user.AccountName;
					userSession.AccountId = user.AccountId;
					userSession.AccountRoleId = user.AccountRoleId;
					Session.Add("UserSession", userSession);
					return Redirect("/");
				}
				else if (result == 0)
				{
					ViewBag.MessageLogin += "Tài khoản không tồn tại";
				}
				else if (result == 2)
				{
					ViewBag.MessageLogin += "Mật khẩu không đúng";
				}
				else if (result == 1)
				{
					ViewBag.MessageLogin += "Đăng nhập thành công";
				}
				else
				{
					ViewBag.MessageLogin += "Đăng nhập không thành công";
				}
			}
			return View(loginViewModel);
		}

		public ActionResult Logout()
		{
			Session["UserSession"] = null;
			return Redirect("/");
		}

		[HttpGet]
		public ActionResult Register()
		{
			RegisterViewModel register = new RegisterViewModel();
			ViewBag.MessageRegister = "";
			return View(register);
		}

		[HttpPost]
		public ActionResult Register(RegisterViewModel register)
		{
			ViewBag.MessageRegister = "";
			// Kiểm tra dữ liệu
			var registerService = new RegisterService();
			var a = ModelState.IsValid;
			if (ModelState.IsValid)
			{
				if (registerService.IsExistAccount(register.AccountName))
				{
					ViewBag.MessageRegister += "Tài khoản đã tồn tại !";
					return View(register);
				}
				if (registerService.IsExistPhone(register.Phone))
				{
					ViewBag.MessageRegister += "Số điên thoại đã tồn tại !";
					return View(register);
				}
				if (registerService.IsExistEmail(register.Email))
				{
					ViewBag.MessageRegister += "Email này đã tồn tại !";
					return View(register);
				}
				// Call method create account
				registerService.RegisterAccount(register);
				ViewData["SuccessMsg"] = "Đăng ký thành công";
			}
			return View(register);
		}


		public ActionResult Detail(int a)
		{
			var account = _accountService.GetById(a);
			var user = _context.Users.SingleOrDefault(s => s.AccountId == account.AccountId);
			if (user == null)
			{
				HttpNotFound();
			}
			return View(user);
		}

		[HttpPost]
		public ActionResult Detail(User user)
		{

			return View(user);
		}
		public ActionResult ChangePassword()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ChangePassword(ChangePasswordViewModel changePasswordViewModel)
		{
			ViewBag.Message = "";
			if (!ModelState.IsValid)
			{
				return View(changePasswordViewModel);
			}
			var session = (UserLogin)Session["UserSession"];
			var account = _context.Accounts.SingleOrDefault(m => m.AccountId == session.AccountId);
			if(account==null)
			{
				HttpNotFound();
			}
			if(account.Password!=changePasswordViewModel.OldPassword)
			{
				ViewBag.Message += "Mật khẩu cũ không đúng,vui lòng nhập lại";
			}	
			account.Password = changePasswordViewModel.NewPassword;
			account.ModifiedDate = DateTime.Now;
			_context.Accounts.AddOrUpdate(account);
			_context.SaveChanges();
			ViewBag.Message += "Đổi mật khẩu thành công";
			return View(changePasswordViewModel);
		}
		public ActionResult UserMenu()
		{
			return PartialView();
		}
	}
}