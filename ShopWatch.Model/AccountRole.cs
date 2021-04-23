using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWatch.Model
{
	public class AccountRole
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AccountRoleId { get; set; }

		[Required(ErrorMessage = "Please enter {0} name.")]
		[StringLength(100, ErrorMessage = "The {0} must be between 10 and 100 characters.")]
		[Display(Name = "Role")]
		public string RoleName { get; set; }

		[Required(ErrorMessage = "Please enter {0} description.")]
		[StringLength(1000, ErrorMessage = "The {0} description must be between 20 and 1000 characters.")]
		[Display(Name = "Description")]
		public string Description { get; set; }
	}
}
