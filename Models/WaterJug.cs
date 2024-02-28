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

        /// <summary>
        /// Water jug builder. Requires values for currentAmount and capacity attributes.
        /// </summary>
        /// <param name="currentAmount">Amount of liquid currently in the jug.</param>
        /// <param name="capacity">Maximum amount of liquid allowed in the jug.</param>
        public WaterJug(int currentAmount, int capacity)
        {
            CurrentAmount = currentAmount;
            Capacity = capacity;
        }

        /// <summary>
        /// Method to fill the jug to the maximum of liquid,
        /// </summary>
        public void Fill()
        {
            CurrentAmount = Capacity;
        }

        /// <summary>
        /// Method for emptying the jug.
        /// </summary>
        public void Empty()
        {
            CurrentAmount = 0;
        }

        /// <summary>
        /// Method of transferring liquid from one jug to another.
        /// </summary>
        /// <param name="otherJug">Jug that will receive the liquid.</param>
        /// <returns>Jug with the liquid received.</returns>
        public WaterJug Transfer(WaterJug otherJug)
        {
            int amountPoured = Math.Min(CurrentAmount, otherJug.Capacity - otherJug.CurrentAmount);
            CurrentAmount -= amountPoured;
            otherJug.CurrentAmount += amountPoured;
            return otherJug;
        }

    }

}
