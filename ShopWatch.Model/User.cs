using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWatch.Model
{
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter your full name.")]
        [StringLength(30, ErrorMessage = "The full name must be between 3 and 30 characters.", MinimumLength = 3)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your city.")]
        [StringLength(30, ErrorMessage = "The city name must be between 3 and 30 characters.", MinimumLength = 3)]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(100, ErrorMessage = "The address must be between 5 and 100 characters.", MinimumLength = 5)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your birth day.")]
        [Display(Name = "Birth Day")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Please enter your email.")]
        [StringLength(50, ErrorMessage = "The email must be between 50 and 5 characters.", MinimumLength = 10)]
		public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone.")]
        [StringLength(20, ErrorMessage = "The email must be between 20 and 5 characters.", MinimumLength = 5)]
        public string Phone { get; set; }

		public virtual ICollection<Order> Orders { get; set; }

        [ForeignKey("Account")]
		public int AccountId { get; set; }
		public virtual Account Account { get; set; }
	}
}
