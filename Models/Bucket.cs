using System.ComponentModel.DataAnnotations;

namespace WaterJugChallenge.Models
{
    public class Bucket
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

        public Bucket Transfer(Bucket otherBucket)
        {
            int amountPoured = Math.Min(CurrentAmount, otherBucket.Capacity - otherBucket.CurrentAmount);
            CurrentAmount -= amountPoured;
            otherBucket.CurrentAmount += amountPoured;
            return otherBucket;
        }

    }

}
