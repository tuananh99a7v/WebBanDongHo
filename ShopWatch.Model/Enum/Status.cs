using System.ComponentModel;

namespace ShopWatch.Model.Enum
{
	public enum Status
	{
		[Description("Chờ giao hàng")]
		WaitingDelivery,

		[Description("Đang giao hàng")]
		Delivering,

		[Description("Đã hoàn thành")]
		Finish,

		[Description("Đã hủy")]
		Cancelled
	}
}
