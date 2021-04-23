using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopWatch.WebMvc.ViewModels
{
	public class WatchEditViewModel
	{
        public int WatchId { get; set; }

        [Required(ErrorMessage = "Please enter {0}.")]
        [Display(Name = "Watch Name")]
        [StringLength(200, ErrorMessage = "The watch name must be between 3 and 200 characters.", MinimumLength = 3)]
        public string WatchName { get; set; }

        [Required(ErrorMessage = "Please select watch big image.")]
        [StringLength(50, ErrorMessage = "The Image must be between 3 and 50 characters.", MinimumLength = 3)]
        [Display(Name = "Image big")]
        public string ImageUrl { get; set; }

        [StringLength(50, ErrorMessage = "The folder image must be between 3 and 50 characters.", MinimumLength = 3)]
        [Display(Name = "Image small")]
        public string SmallImage { get; set; }

        [StringLength(1000, ErrorMessage = "The description must be between 5 and 1000 characters.", MinimumLength = 5)]
        public string Description { get; set; }


        [StringLength(1000, ErrorMessage = "The evaluate must be between 5 and 1000 characters.", MinimumLength = 5)]
        public string Evaluate { get; set; }

        [Required(ErrorMessage = "Please enter watch price")]
        [Display(Name = "Price")]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter watch promotion price")]
        [Display(Name = "Price Promotion")]
        [DefaultValue(0)]
        public decimal PricePromotion { get; set; }

        [Required(ErrorMessage = "Please enter quantity")]
        [Display(Name = "Quantity")]
        [DefaultValue(0)]
        public int Quantity { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "Is Active?")]
        [DefaultValue(false)]
        public bool IsActive { get; set; }

        public int CategoryId { get; set; }
        public virtual CategoryViewModel Category { get; set; }

        public int PublisherId { get; set; }

        public virtual PublisherViewModel Publisher { get; set; }

        public virtual ICollection<PromotionViewModel> Promotions { get; set; }
    }
}