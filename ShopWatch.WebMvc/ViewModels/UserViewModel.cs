using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWatch.WebMvc.ViewModels
{
	public class UserViewModel
	{
		public int UserId { get; set; }
		public string Name { get; set; }
        
        public string City { get; set; }
        
        public string Address { get; set; }
        
        public DateTime BirthDate { get; set; }
       
        public string Photo { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }

		public AccountViewModel Account{ get; set; }

	}
}