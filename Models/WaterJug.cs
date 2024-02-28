using System.ComponentModel.DataAnnotations;

namespace WaterJugChallenge.Models
{
    /// <summary>
    /// Class representing the data model for the water jug.
    /// </summary>
    public class WaterJug
    {

        /// <summary>
        /// Amount of liquid currently in the jug.
        /// </summary>
        [Required]
        public int CurrentAmount { get; set; } = 0;

        /// <summary>
        /// Maximum amount of liquid allowed in the jug.
        /// </summary>
        [Required]
        public int Capacity { get; set; } = 0;

        /// <summary>
        /// Empty water jug builder. Initializes its attributes to zero.
        /// </summary>
        public WaterJug() { }

    }

}
