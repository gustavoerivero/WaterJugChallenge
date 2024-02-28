using System.ComponentModel.DataAnnotations;

namespace WaterJugChallenge.Models
{
    public class Capacities
    {
        [Required]
        public int XCapacity { get; set; } = 0;

        [Required]
        public int YCapacity { get; set; } = 0;

        [Required]
        public int ZTarget { get; set; } = 0;
    }
}
