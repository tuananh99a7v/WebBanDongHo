using ShopWatch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWatch.BussinessLogicLayer.IService
{
	public interface ICheckoutService
	{
		void Checkout(Order order, List<OrderDetail> orderDetails);

	}
}
