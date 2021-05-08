using ShopWatch.Model;
using System.Collections.Generic;

namespace ShopWatch.WebMvc.Areas.Admin.ViewModel
{
	public class DashboardViewModel
	{
		public IEnumerable<Order> OrderWating { get; set; }
		public IEnumerable<Watch> WatchesEnd{ get; set; }
		public IEnumerable<Watch> WatchesOrder { get; set; }
		public decimal TotalPriceOrder { get; set ; }
		public int TotalWatchesEnd { get; set; }
		public int TotalOrderWaiting { get; set; }

	}
}