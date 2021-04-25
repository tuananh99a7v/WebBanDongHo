using System.ComponentModel.DataAnnotations;

namespace ShopWatch.WebMvc.ViewModels
{
	public class WatchPromotionViewModel
	{
		[Required(ErrorMessage ="Không được để trống")]
		public int PromotionId { get; set; }

		[Required(ErrorMessage = "Không được để trống")]
		public int WatchId { get; set; }
	}
}