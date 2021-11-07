using System.ComponentModel.DataAnnotations;

namespace MVCWebApplication.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Category Name")]
        public int ProductCategoryId { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Category")]
        public Category ProductCategory { get; set; }
    }
}