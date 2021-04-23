using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWatch.WebMvc.ViewModels
{
	public class CategoryViewModel
	{
		public string CategoryName { get; set; }

		public string Description { get; set; }

		public virtual ICollection<WatchViewModel> Watches { get; set; }
	}
}