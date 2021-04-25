using ShopWatch.BussinessLogicLayer.IGennericRepository;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.Model;
using System;
using System.Collections.Generic;



namespace ShopWatch.BussinessLogicLayer.Services
{
	public class CheckoutService : ICheckoutService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<Order> _orderRepository;
		private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
		private readonly IGenericRepository<Watch> _watchRepository;

		public CheckoutService(
			IUnitOfWork unitOfWork,
			IGenericRepository<Order> orderRepository,
			IGenericRepository<OrderDetail> orderDetailRepository,
			IGenericRepository<Watch> bookRepository)
		{
			_unitOfWork = unitOfWork;
			_orderRepository = orderRepository;
			_orderDetailRepository = orderDetailRepository;
			_watchRepository = bookRepository;
		}
		public void Checkout(Order order, List<OrderDetail> orderDetails)
		{
			//Custom trang thái order
			order.CreatedDate = DateTime.Now;
			order.ShippedDate = DateTime.Now.AddDays(4);
			order.ModifiedDate = DateTime.Now;
			order.Status = Model.Enum.Status.WaitingDelivery;

			_orderRepository.Add(order);

			foreach (var orderDetail in orderDetails)
			{
				//Tìm sản phẩm muốn trừ số lượng
				var watch = _watchRepository.GetById(orderDetail.WatchId);
				//trừ số lượng
				watch.Quantity -= orderDetail.Quantity;
				_watchRepository.Update(watch); //Update tại bảng watch ứng với watchId

				orderDetail.Order = order;
				_orderDetailRepository.Add(orderDetail);
			}
			_unitOfWork.Commit();
			
		}
	}
}
