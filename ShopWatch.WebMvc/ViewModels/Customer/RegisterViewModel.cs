using System;
using System.ComponentModel.DataAnnotations;

namespace ShopWatch.WebMvc.ViewModels.Customer
{
	public class RegisterViewModel
	{
        [Display(Name ="Họ và tên")]
        [Required(ErrorMessage = "Bạn cần nhập tên")]
        public string Name { get; set; }

        [Display(Name = "Thành phố")]
        [Required(ErrorMessage = "Bạn cần nhập thành phố")]
        public string City { get; set; }

        [Display(Name = "Ngày sinh")]

        [Required(ErrorMessage = "Bạn cần nhập ngày sinh")]
        public DateTime BirthDate { get; set; }

        [Display(Name ="Địa chỉ")]
        [Required(ErrorMessage = "Bạn cần nhập địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Bạn cần nhập email")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Bạn cần nhập tên đăng nhập")]
        public string AccountName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { get; set; }

        [Display(Name = "Nhập lại mật khẩu")]
        [Required(ErrorMessage = "Bạn cần nhập lại mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
		[Compare("Password", ErrorMessage = "Xác nhận mật khẩu không chính xác")]

        public string ConfirmPassword { get; set; }
    }
}