using ShopWatch.BussinessLogicLayer.IGennericRepository;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.BussinessLogicLayer.Service;
using ShopWatch.Model;
using ShopWatch.Model.DataContext;

namespace ShopWatch.BussinessLogicLayer.Services
{
	public class PublisherService:BaseService<Publisher>,IPublisherService
	{
		public PublisherService(IUnitOfWork unitOfWork, IGenericRepository<Publisher> repository) : base(unitOfWork, repository)
		{

		}
	}
}
