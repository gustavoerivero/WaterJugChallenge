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
        public int? XCapacity { get; set; }

        /// <summary>
        /// Maximum amount of liquid allowed in the Y jug.
        /// </summary>
        public int? YCapacity { get; set; }

        /// <summary>
        /// Value to be searched.
        /// </summary>
        public int? ZTarget { get; set; }
    }
}
