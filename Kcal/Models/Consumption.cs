using System.ComponentModel.DataAnnotations;

namespace Kcal.Models
{
    public class Consumption
    {
        public uint Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0, 10000)]
        public double TotalKcal { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
