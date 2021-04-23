using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWatch.Model
{
	public class Category
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Please enter category name.")]
		[StringLength(100, ErrorMessage = "The category name must be between 5 and 100 characters.", MinimumLength = 5)]
		[Display(Name = "Category Name")]
		public string CategoryName { get; set; }

		[Required(ErrorMessage = "Please enter category description.")]
		[StringLength(1000, ErrorMessage = "The category description must be between 5 and 1000 characters.", MinimumLength = 5)]
		[Display(Name = "Description")]
		public string Description { get; set; }

		public virtual ICollection<Watch> Watches{ get; set; }
	}
}
