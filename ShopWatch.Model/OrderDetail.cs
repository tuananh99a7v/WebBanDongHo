using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWatch.Model
{
	public class OrderDetail
	{
        [Key]
        [Column(Order = 1)]
        public int OrderId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int WatchId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        [ForeignKey("WatchId")]
        public virtual Watch Watch { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
