using System.ComponentModel.DataAnnotations;

namespace Kcal.Models
{
    public class ProductCategory
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}
