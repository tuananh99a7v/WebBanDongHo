using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWatch.Model
{
	public class Publisher
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PublisherId { get; set; }

		[Required(ErrorMessage = "Please enter {0}.")]
		[StringLength(100, ErrorMessage = "The {0} must be between 2 and 100 characters.", MinimumLength = 2)]
		[Display(Name = "Publisher Name")]
		public string PublisherName { get; set; }

		[Required(ErrorMessage = "Please enter {0}.")]
		[StringLength(1000, ErrorMessage = "The {0} must be between 5 and 1000 characters.", MinimumLength = 5)]
		[Display(Name = "Description")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Please enter {0}.")]
		[StringLength(100, ErrorMessage = "The {0} must be between 1 and 100 characters.", MinimumLength = 1)]
		[Display(Name = "Image")]
		public string Image { get; set; }

		public virtual ICollection<Watch> Watches { get; set; }
	}
}
