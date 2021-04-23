using ShopWatch.Model;
using System;

namespace ShopWatch.WebMvc.ViewModels.Customer
{
	[Serializable]
	public class ShoppingCartItem
	{
		public int WatchId { get; set; }

		public decimal Price { get; set; }

		public int Quantity { get; set; }

		public decimal Total { get { return Quantity * Price; } }

		public virtual Watch Watch { get; set; }

	}
}