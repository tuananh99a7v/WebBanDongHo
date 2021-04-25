using ShopWatch.Model;
using ShopWatch.Model.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWatch.BussinessLogicLayer.Services
{
	public class PromotionDetailService
	{
		private readonly ShopWatchDataContext _context=new ShopWatchDataContext();
		
		//public int UpdatePrice(Watch watch)
		//{
		//	watch.Promotions.
		//}
		public bool CheckEndDate(Promotion promotion)
		{
			if (promotion.EndDate < DateTime.Now) return false;
			return true;
		}
		public bool PublicPromotion(Promotion promotion)
		{
			if (promotion.StartDate <= DateTime.Now && promotion.EndDate >= DateTime.Now) return true;
			return false;
		}
		public bool OneWatch(Promotion promotion)
		{
			if (promotion.Watches.Count() > 1) return false;
			return true;
		}
	}
}
