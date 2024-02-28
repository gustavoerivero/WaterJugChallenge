using System.ComponentModel.DataAnnotations;

namespace WaterJugChallenge.Models
{
    public class WaterJug
    {

        [Required]
        public int CurrentAmount { get; set; } = 0;

        [Required]
        public int Capacity { get; set; } = 0;

        public void Fill()
        {
            CurrentAmount = Capacity;
        }

        public void Empty()
        {
            CurrentAmount = 0;
        }

        public WaterJug Transfer(WaterJug otherBucket)
        {
            int amountPoured = Math.Min(CurrentAmount, otherBucket.Capacity - otherBucket.CurrentAmount);
            CurrentAmount -= amountPoured;
            otherBucket.CurrentAmount += amountPoured;
            return otherBucket;
        }

    }

}
