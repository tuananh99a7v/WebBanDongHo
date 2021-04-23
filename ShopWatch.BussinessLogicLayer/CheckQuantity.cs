using ShopWatch.Model.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWatch.BussinessLogicLayer
{
	public class CheckQuantity
	{
		private static readonly ShopWatchDataContext _context = new ShopWatchDataContext();
		public static bool Check(int id, int quantity)
		{
			var product = _context.Watches.Find(id);
			if (quantity > product.Quantity) return false;
			return true;
		}
	}
}
