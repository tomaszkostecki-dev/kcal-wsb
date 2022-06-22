using System.ComponentModel.DataAnnotations;

namespace Kcal.Models
{
    public class Product
    {
        public uint Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0, 10000)]
        public double KcalPer100g { get; set; }
    }
}
