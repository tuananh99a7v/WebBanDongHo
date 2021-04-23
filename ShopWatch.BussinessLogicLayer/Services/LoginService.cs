using ShopWatch.Model;
using ShopWatch.Model.DataContext;
using System.Linq;

namespace ShopWatch.BussinessLogicLayer.Services
{
	public class LoginService
	{
        ShopWatchDataContext db = null;
        public LoginService()
        {
            db = new ShopWatchDataContext();
        }

        /// <summary>
        /// Service lấy username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Account GetById(string userName)
        {
            return db.Accounts.SingleOrDefault(n => n.AccountName == userName);
        }

        /// <summary>
        /// Service kiểm tra username và password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public int Login(string userName, string passWord)
        {
            var result = db.Accounts.SingleOrDefault(n => n.AccountName == userName);
            if (result == null)
            {
                return 0;
            }
            if (result.Password == passWord) return 1;
                else return 2;
        }
    }
}
