using ShopWatch.Model;
using System.Collections.Generic;

namespace ShopWatch.WebMvc.ViewModels.Customer
{
	public class WatchListByCategoryViewModel
	{
		public IEnumerable<Watch> Watches { get; set; }
		public string CurrentCategory { get; set; }

	}
}