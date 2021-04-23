using System.Collections.Generic;

namespace ShopWatch.WebMvc.ViewModels
{
	public class PublisherViewModel
	{
		public string PublisherName { get; set; }
		
		public string Description { get; set; }
		
		public string Image { get; set; }

		public virtual ICollection<WatchViewModel> Watches { get; set; }
	}
}