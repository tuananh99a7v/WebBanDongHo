using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWatch.Model
{
	public class Account
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AccountId { get; set; }

		[Required(ErrorMessage = "Please enter account name.")]
		[StringLength(50, ErrorMessage = "The category name must be between 10 and 50 characters.", MinimumLength = 5)]
		[Display(Name = "Account Name")]
		public string AccountName { get; set; }

		[Required(ErrorMessage = "Please enter password.")]
		[StringLength(50, ErrorMessage = "The password must be between 5 and 50 characters.", MinimumLength = 5)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Please enter create date.")]
		public DateTime CreateDate { get; set; }

		[Required(ErrorMessage = "Please enter modified date.")]
		public DateTime ModifiedDate { get; set; }

		[ForeignKey("AccountRole")]
		public int AccountRoleId { get; set; }
		public virtual AccountRole AccountRole { get; set; }
	}
}
