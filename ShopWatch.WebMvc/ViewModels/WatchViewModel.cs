using ShopWatch.Model;
using System;
using System.Collections.Generic;


namespace ShopWatch.WebMvc.ViewModels
{
	public class WatchViewModel
	{
        public int WatchId { get; set; }

        public string WatchName { get; set; }
        
        public string ImageUrl { get; set; }
        
        public string SmallImage { get; set; }

        public string Description { get; set; }

        public string Evaluate { get; set; }
        
        public decimal Price { get; set; }

        public decimal PricePromotion { get; set; }

        public int Quantity { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime ModifiedDate { get; set; }
        
        public bool IsActive { get; set; }
        
        public int CategoryId { get; set; }

        public  CategoryViewModel Category { get; set; }

        public int PublisherId { get; set; }

        public  PublisherViewModel Publisher { get; set; }

		public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }

        public virtual IEnumerable<Promotion> Promotions { get; set; }
	}
}