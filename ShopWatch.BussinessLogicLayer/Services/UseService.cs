using ShopWatch.BussinessLogicLayer.IGennericRepository;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.BussinessLogicLayer.Service;
using ShopWatch.Model;
using ShopWatch.Model.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWatch.BussinessLogicLayer.Services
{
	public class UserService : BaseService<User>, IUserService
	{
		public UserService(IUnitOfWork unitOfWork, IGenericRepository<User> repository) : base(unitOfWork, repository)
		{

		}
	}
}
