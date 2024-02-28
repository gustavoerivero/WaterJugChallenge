namespace WaterJugChallenge.Models
{
    public class Response<T>
    {
        public string Message { get; set; } = string.Empty;

        public object? Data { get; set; }

    }
}
