using System.ComponentModel.DataAnnotations;

namespace WaterJugChallenge.Models
{
    /// <summary>
    /// Class containing the model that the endpoint will receive in order to solve the water jug challenge.
    /// </summary>
    public class Capacities
    {
        /// <summary>
        /// Maximum amount of liquid allowed in the X jug.
        /// </summary>
        [Required]
        public int XCapacity { get; set; } = 0;

        /// <summary>
        /// Maximum amount of liquid allowed in the Y jug.
        /// </summary>
        [Required]
        public int YCapacity { get; set; } = 0;

        /// <summary>
        /// Value to be searched.
        /// </summary>
        [Required]
        public int ZTarget { get; set; } = 0;
    }
}
