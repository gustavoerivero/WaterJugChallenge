namespace WaterJugChallenge.Models
{
    /// <summary>
    /// Class that represents the status model of a specific step in the solution of the challenge.
    /// </summary>
    public class State
    {
        /// <summary>
        /// Amount of contents in the X jug.
        /// </summary>
        public int JugXVolume { get; private set; }

        /// <summary>
        /// Amount of contents in the Y jug.
        /// </summary>
        public int JugYVolume { get; private set; }

        /// <summary>
        /// Action taken for the relevant step.
        /// </summary>
        public string Action { get; set; }

        /// Constructs a new state with the specified volumes for jug X and jug Y.
        /// </summary>
        /// <param name="jugXVolume">The volume of water in jug X.</param>
        /// <param name="jugYVolume">The volume of water in jug Y.</param>
        public State(int jugXVolume, int jugYVolume)
        {
            JugXVolume = jugXVolume;
            JugYVolume = jugYVolume;
            Action = String.Empty;
        }

        /// <summary>
        /// Determines whether the current state is equal to another state.
        /// </summary>
        /// <param name="other">The other state to compare.</param>
        /// <returns>True if the states are equal, false otherwise.</returns>
        public override bool Equals(object? other)
        {
            if (other == null || GetType() != other.GetType())
            {
                return false;
            }

            State otherState = (State)other;
            return JugXVolume == otherState.JugXVolume && JugYVolume == otherState.JugYVolume;
        }

        /// <summary>
        /// Returns the hash code for the current state.
        /// </summary>
        /// <returns>The hash code for the current state.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(JugXVolume, JugYVolume);
        }
    }
}
