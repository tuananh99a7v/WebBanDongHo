using ShopWatch.BussinessLogicLayer.IGennericRepository;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.BussinessLogicLayer.Service;
using ShopWatch.Model;
using ShopWatch.Model.DataContext;

namespace ShopWatch.BussinessLogicLayer.Services
{
	public class PromotionService:BaseService<Promotion>, IPromotionService
	{
		public PromotionService(IUnitOfWork unitOfWork, IGenericRepository<Promotion> repository):base(unitOfWork,repository)
		{

		}
	}
}
