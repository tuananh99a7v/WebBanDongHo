using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWatch.WebMvc.ViewModels
{
	public class PromotionViewModel
	{
		public int PromotionId { get; set; }

		public string PromotionName { get; set; }

		public int Percent { get; set; }


		public DateTime StartDate { get; set; }


		public DateTime EndDate { get; set; }
	}
}