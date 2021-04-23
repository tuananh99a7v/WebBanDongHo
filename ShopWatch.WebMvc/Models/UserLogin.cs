using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWatch.WebMvc.Models
{
	[Serializable]
	public class UserLogin
	{
		public int AccountId { set; get; }
		public string AccountName { set; get; }
		public int AccountRoleId { get; set; }
	}
}