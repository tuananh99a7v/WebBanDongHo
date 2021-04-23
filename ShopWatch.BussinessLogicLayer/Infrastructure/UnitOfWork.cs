using ShopWatch.Model.DataContext;
using System.Threading.Tasks;

namespace ShopWatch.BussinessLogicLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private ShopWatchDataContext _context;

        public ShopWatchDataContext DbContext => _context ?? (_context = _dbFactory.Init());

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public int Commit()
        {
            return DbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return DbContext.SaveChangesAsync();
        }
    }
}
