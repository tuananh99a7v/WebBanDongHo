using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopWatch.WebMvc.ViewModels
{
	public class ChangePasswordViewModel
	{

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Mật khẩu cũ")]
		public string OldPassword { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Mật khẩu mới")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Xác nhận mật khẩu")]
		[Compare("NewPassword", ErrorMessage = "Mật khẩu nhập lại không giống")]
		public string ConfirmPassword { get; set; }
	}

}