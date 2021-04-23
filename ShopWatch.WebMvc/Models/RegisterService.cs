using ShopWatch.Model;
using ShopWatch.Model.DataContext;
using ShopWatch.WebMvc.ViewModels.Customer;
using System;
using System.Linq;

namespace ShopWatch.WebMvc.Models
{
	public class RegisterService
	{
		private ShopWatchDataContext _context;
		public RegisterService()
		{
			_context = new ShopWatchDataContext();
		}
		public bool IsExistAccount(string accountName)
		{
			var a = _context.Accounts.SingleOrDefault(m => m.AccountName.Equals(accountName));
			if (a != null) return true;
			return false;
		}
		public bool IsExistPhone(string phone)
		{
			var a = _context.Users.SingleOrDefault(m => m.Phone.Equals(phone));
			if (a != null) return true;
			return false;
		}
		public bool IsExistEmail(string email)
		{
			var a = _context.Users.SingleOrDefault(m => m.Email.Equals(email));
			if (a != null) return true;
			return false;
		}
		public void RegisterAccount(RegisterViewModel registerViewModel )
		{
			var account = new Account()
			{
				AccountName=registerViewModel.AccountName,
				Password=registerViewModel.Password,
				CreateDate=DateTime.Now,
				ModifiedDate=DateTime.Now,
				AccountRoleId=1
			};
			_context.Accounts.Add(account);
			_context.SaveChanges();
			var user = new User()
			{
				Name=registerViewModel.Name,
				City=registerViewModel.City,
				Address=registerViewModel.Address,
				BirthDate=registerViewModel.BirthDate,
				Phone=registerViewModel.Phone,
				Email=registerViewModel.Email,
				Photo="",
				Account=account
			};
			_context.Users.Add(user);
			_context.SaveChanges();
		}
		
	}
}