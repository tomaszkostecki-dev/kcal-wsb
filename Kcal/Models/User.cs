using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Kcal.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Consumptions = new List<Consumption>();
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        [Range(1, Double.MaxValue)]
        public double Weight { get; set; }
        [Required]
        [Range(1, uint.MaxValue)]
        public uint Height { get; set; }
        [Range(1, uint.MaxValue)]
        [Required]
        public uint Age { get; set; }

        public virtual ICollection<Consumption> Consumptions { get; set; }

    }
}
