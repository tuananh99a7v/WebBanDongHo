using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopWatch.WebMvc.ViewModels
{
	public class PublisherEditViewModel
	{
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
	
	}
}