using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopWatch.WebMvc.ViewModels
{
	public class PromotionEditViewModel
	{
		public int PromotionId { get; set; }

		[Display(Name = "Promotion Name")]
		[Required(ErrorMessage = "Không được để trống")]
		public string PromotionName { get; set; }
		[Required]
		[DefaultValue(0)]
		public int Percent { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

		[Required]
		public DateTime EndDate { get; set; }
	}
}