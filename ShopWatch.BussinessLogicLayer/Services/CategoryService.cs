using ShopWatch.BussinessLogicLayer.IGennericRepository;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.BussinessLogicLayer.Service;
using ShopWatch.Model;

namespace ShopWatch.BussinessLogicLayer.Services
{
	public class CategoryService:BaseService<Category>,ICategoryService
	{
		public CategoryService(IUnitOfWork unitOfWork, IGenericRepository<Category> repository) :base(unitOfWork,repository)
		{
		}
	}
}
