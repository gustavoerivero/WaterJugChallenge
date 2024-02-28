namespace WaterJugChallenge.Models
{
    /// <summary>
    /// Template of the response to be returned to the user.
    /// </summary>
    /// <typeparam name="T">object</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Message indicating whether the challenge has been solved, has no solution or any additional message.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Steps to reach the solution of the water jug challenge or null in case it cannot be solved.
        /// </summary>
        public object? Data { get; set; }

    }
}
