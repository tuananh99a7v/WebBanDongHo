using System.Threading.Tasks;

namespace ShopWatch.BussinessLogicLayer
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
