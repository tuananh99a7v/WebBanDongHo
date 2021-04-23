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
	public class WatchService: BaseService<Watch>, IWatchService
	{
		public WatchService(IUnitOfWork unitOfWork, IGenericRepository<Watch> repository) : base(unitOfWork, repository)
		{

		}
		public override int Create(Watch entity)
		{
			return base.Create(entity);
		}
	}
}

