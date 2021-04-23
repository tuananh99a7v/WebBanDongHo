using System.ComponentModel.DataAnnotations;

namespace ShopWatch.WebMvc.ViewModels
{
	public class CategoryEditViewModel
	{
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter category name.")]
        [MaxLength(100, ErrorMessage = "The category name must be between 5 and 100 characters.")]
        [MinLength(5, ErrorMessage = "The category name must be between 5 and 100 characters.")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Please enter category description.")]
        [MaxLength(1000, ErrorMessage = "The category description must be between 5 and 1000 characters.")]
        [MinLength(5, ErrorMessage = "The category description must be between 5 and 1000 characters.")]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}