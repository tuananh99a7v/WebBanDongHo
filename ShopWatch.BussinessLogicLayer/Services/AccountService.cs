using ShopWatch.BussinessLogicLayer.IGennericRepository;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.BussinessLogicLayer.Service;
using ShopWatch.Model;

namespace ShopWatch.BussinessLogicLayer.Services
{
	public class AccountService:BaseService<Account>,IAccountService
	{
		public AccountService(IUnitOfWork unitOfWork, IGenericRepository<Account> repository) : base(unitOfWork, repository)
		{

		}
	}
}
