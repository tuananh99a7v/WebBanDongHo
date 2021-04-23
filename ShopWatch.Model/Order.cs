using ShopWatch.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWatch.Model
{
	public class Order
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "Shipped Date")]
        public DateTime ShippedDate { get; set; }

        [Required(ErrorMessage = "Please enter Status.")]
        [Display(Name = "Status")]
        public Status Status{ get; set; }

        [Required(ErrorMessage = "Please enter your ship address.")]
        [StringLength(100, ErrorMessage = "The ship address must be between 5 and 100 characters", MinimumLength = 5)]
        [Display(Name = "Ship Address")]
        public string ShipAddress { get; set; }

        [Required(ErrorMessage = "Please enter your phone number.")]
        [StringLength(10, ErrorMessage = "The phone number must be 10 characters", MinimumLength = 10)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [ForeignKey("User")]
		public int UserId { get; set; }
		public virtual User User { get; set; }
		public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
