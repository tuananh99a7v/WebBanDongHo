using ShopWatch.Model.DataContext;
using System;

namespace ShopWatch.BussinessLogicLayer
{
    public interface IDbFactory: IDisposable
    {
        ShopWatchDataContext Init();
    }
}
