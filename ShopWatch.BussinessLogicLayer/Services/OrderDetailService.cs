using ShopWatch.BussinessLogicLayer.IGennericRepository;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.BussinessLogicLayer.Service;
using ShopWatch.Model;
using ShopWatch.Model.DataContext;

namespace ShopWatch.BussinessLogicLayer.Services
{
	public class OrderDetailService : BaseService<OrderDetail>, IOrderDetailService
	{
		public OrderDetailService(IUnitOfWork unitOfWork, IGenericRepository<OrderDetail> repository) : base(unitOfWork, repository)
		{

		}


	}
}
