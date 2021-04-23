using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopWatch.WebMvc.ViewModels
{
	public class LoginViewModel
	{

		[Required(ErrorMessage = "Bạn cần nhập tài khoản")]
		[Display(Name = "Tên đăng nhập")]
		public string AccountName { get; set; }

		[Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
		[DataType(DataType.Password)]
		[Display(Name = "Mật khẩu")]

		public string PassWord { get; set; }

		public Boolean RememberMe { get; set; }
	}
}