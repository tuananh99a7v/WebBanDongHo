using ShopWatch.Model.Enum;
using System;

namespace ShopWatch.WebMvc.ViewModels
{
	public class OrderViewModel
	{

        public Status Status { get; set; }

        public string ShipAddress { get; set; }

        public string PhoneNumber { get; set; }

	}
}