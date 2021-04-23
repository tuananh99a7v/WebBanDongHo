using ShopWatch.Model;
using ShopWatch.Model.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
	class Program
	{
		private static readonly ShopWatchDataContext dba = new ShopWatchDataContext();
		private static readonly ShopWatchDataContext dba2 = new ShopWatchDataContext();

		static void Main(string[] args)
		{
			var shop = dba.Users.Find(3);
			shop.Account.Password = "123456789";
			dba.Users.AddOrUpdate(shop);
			dba.SaveChanges();
			var ui = dba.Accounts.Find(6);
			Console.WriteLine(ui.Password);
			Console.WriteLine();
			Console.ReadKey();

		}


		//try
		//{
		//	Console.OutputEncoding = Encoding.UTF8;
		//	var list = dba.Watches.ToList();

		//	list.ForEach(s => Console.WriteLine(s.WatchName));

		//	Console.WriteLine("cu lồn");
		//	Console.WriteLine(list.Count);
		//	Console.ReadKey();
		//}
		//catch (DbEntityValidationException e)
		//{
		//	Console.WriteLine(e.ToString());
		//}


	}
}
