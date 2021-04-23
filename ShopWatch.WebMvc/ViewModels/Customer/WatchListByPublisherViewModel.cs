using ShopWatch.Model;
using System.Collections.Generic;

namespace ShopWatch.WebMvc.ViewModels.Customer
{
	public class WatchListByPublisherViewModel
	{
		public IEnumerable<Watch> Watches { get; set; }
		public string CurrentPublisher { get; set; }
	}
}