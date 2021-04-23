using ShopWatch.Model.DataContext;

namespace ShopWatch.BussinessLogicLayer
{
    public class DbFactory: Disposable, IDbFactory
    {
        ShopWatchDataContext _dbContext;

        public ShopWatchDataContext Init() => _dbContext ?? (_dbContext = new ShopWatchDataContext());

        protected override void DisposeCore()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
    }
}
