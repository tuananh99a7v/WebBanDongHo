using ShopWatch.BussinessLogicLayer.IGennericRepository;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.BussinessLogicLayer.Service;
using ShopWatch.Model;

namespace ShopWatch.BussinessLogicLayer.Services
{
	public class OrderService : BaseService<Order>, IOrderService
	{
		public OrderService(IUnitOfWork unitOfWork, IGenericRepository<Order> repository) : base(unitOfWork, repository)
		{

		}
		
		
	}
}
