using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWatch.Model
{
	public class Promotion
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PromotionId { get; set; }

		[Display(Name ="PromotionName")]
		[Required(ErrorMessage ="Không được để trống")]
		public string PromotionName { get; set; }
		[Required]
		[DefaultValue(0)]
		public int Percent { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

		[Required]
		public DateTime EndDate { get; set; }

		public virtual ICollection<Watch> Watches { get; set; }

	}
}
