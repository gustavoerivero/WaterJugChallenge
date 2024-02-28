namespace WaterJugChallenge.Models
{
    /// <summary>
    /// Class containing the method to obtain the list of steps that gives the solution to the water jug challenge.
    /// </summary>
    public class WaterJugSolution
    {
        /// <summary>
        /// List of steps taken to reach the solution of the water jug challenge.
        /// </summary>
        public List<State> Steps { get; private set; }

        /// <summary>
        /// Constructs a new water jug solution with the specified steps.
        /// </summary>
        /// <param name="steps">The sequence of steps taken to reach the solution.</param>
        public WaterJugSolution(List<State> steps)
        {
            Steps = steps;
        }

    }
}
