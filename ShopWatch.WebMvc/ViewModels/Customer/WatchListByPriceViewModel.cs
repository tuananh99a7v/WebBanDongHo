using ShopWatch.Model;
using System.Collections.Generic;

namespace ShopWatch.WebMvc.ViewModels.Customer
{
	public class WatchListByPriceViewModel
	{
		public IEnumerable<Watch> Watches { get; set; }
		public decimal CurrentPrice { get; set; }
	}
}