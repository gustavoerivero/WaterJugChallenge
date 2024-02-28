using System.Text.Json.Serialization;

namespace WaterJugChallenge.Models
{
    public class Operation
    {
        public int JugXAmount { get; set; } = 0;

        public int JugYAmount { get; set; } = 0;

        public string Action { get; set; } = string.Empty;

    }
}