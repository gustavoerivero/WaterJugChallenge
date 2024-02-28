using WaterJugChallenge.Models;

namespace WaterJugChallenge.Utils
{
    /// <summary>
    /// Class containing the variables and methods needed to solve the water jug challenge.
    /// </summary>
    public class WaterJugSolver
    {
        /// <summary>
        /// Maximum quantity allowed in X jug.
        /// </summary>
        private readonly int JugXCapacity;

        /// <summary>
        /// Maximum quantity allowed in Y jug.
        /// </summary>
        private readonly int JugYCapacity;

        /// <summary>
        /// Value searched.
        /// </summary>
        private readonly int SearchedValue;

        /// <summary>
        /// Constructs a new water jug solver with the specified jug capacities and searched value.
        /// </summary>
        /// <param name="jugX">The water jug X</param>
        /// <param name="jugY">The water jug Y</param>
        /// <param name="searchedValue">The value to be searched for in the jugs.</param>
        public WaterJugSolver(WaterJug jugX, WaterJug jugY, int searchedValue)
        {
            this.JugXCapacity = jugX.Capacity;
            this.JugYCapacity = jugY.Capacity;

            this.SearchedValue = searchedValue;
        }

        /// <summary>
        /// Solves the water jug challenge using the BFS algorithm.
        /// </summary>
        /// <returns>A WaterJugSolution object representing the optimal solution to the challenge.</returns>
        public WaterJugSolution Solve()
        {
            // Create the initial state with both jugs empty.
            State initialState = new(0, 0);

            // Create a queue to store the states to be explored.
            Queue<State> queue = new();
            queue.Enqueue(initialState);

            // Create a set to store the visited states.
            HashSet<State> visited = new()
            {
                initialState
            };

            // Create a dictionary to store the steps taken to reach each state.
            Dictionary<State, List<State>> stepsTaken = new()
            {
                [initialState] = new()
            };

            // Perform BFS until a solution is found or all states are explored.
            while (queue.Count > 0)
            {
                State currentState = queue.Dequeue();

                // Check if the searched value is found in either jug.
                if (currentState.JugXVolume == SearchedValue || currentState.JugYVolume == SearchedValue)
                {
                    return new WaterJugSolution(stepsTaken[currentState]);
                }

                // Generate all possible next states from the current state.
                List<State> nextStates = GenerateNextStates(currentState);

                foreach (State nextState in nextStates)
                {
                    if (!visited.Contains(nextState))
                    {
                        queue.Enqueue(nextState);
                        visited.Add(nextState);

                        // Update the steps taken to reach the next state.
                        stepsTaken[nextState] = new List<State>(stepsTaken[currentState])
                        {
                            nextState
                        };
                    }
                }
            }

            // If no solution is found, return an empty solution.
            return new WaterJugSolution(new List<State>());
        }

        /// <summary>
        /// Generates all possible next states from a given state.
        /// </summary>
        /// <param name="currentState">The current state.</param>
        /// <returns>A list of all possible next states from the current state.</returns>
        private List<State> GenerateNextStates(State currentState)
        {
            List<State> nextStates = new();

            // Fill jug X.
            if (currentState.JugXVolume < JugXCapacity)
            {
                State nextState = new(JugXCapacity, currentState.JugYVolume)
                {
                    Action = $"Fill X: ({JugXCapacity}, {currentState.JugYVolume})"
                };
                nextStates.Add(nextState);
            }

            // Fill jug Y.
            if (currentState.JugYVolume < JugYCapacity)
            {
                State nextState = new(currentState.JugXVolume, JugYCapacity)
                {
                    Action = $"Fill Y: ({currentState.JugXVolume}, {JugYCapacity})"
                };
                nextStates.Add(nextState);
            }

            // Empty jug X.
            if (currentState.JugXVolume > 0)
            {
                State nextState = new(0, currentState.JugYVolume)
                {
                    Action = $"Empty X: ({0}, {currentState.JugYVolume})"
                };
                nextStates.Add(nextState);
            }

            // Empty jug Y.
            if (currentState.JugYVolume > 0)
            {
                State nextState = new(currentState.JugXVolume, 0)
                {
                    Action = $"Empty Y: ({currentState.JugXVolume}, {0})"
                };
                nextStates.Add(nextState);
            }

            // Pour from jug X to jug Y.
            if (currentState.JugXVolume > 0 && currentState.JugYVolume < JugYCapacity)
            {
                int spaceAvailable = JugYCapacity - currentState.JugYVolume;
                int waterToPour = Math.Min(currentState.JugXVolume, spaceAvailable);

                State nextState = new(currentState.JugXVolume - waterToPour, currentState.JugYVolume + waterToPour)
                {
                    Action = $"Transfer X to Y: ({currentState.JugXVolume - waterToPour}, {currentState.JugYVolume + waterToPour})"
                };
                nextStates.Add(nextState);
            }

            // Pour from jug Y to jug X.
            if (currentState.JugYVolume > 0 && currentState.JugXVolume < JugXCapacity)
            {
                int spaceAvailable = JugXCapacity - currentState.JugXVolume;
                int waterToPour = Math.Min(currentState.JugYVolume, spaceAvailable);

                State nextState = new(currentState.JugXVolume + waterToPour, currentState.JugYVolume - waterToPour)
                {
                    Action = $"Transfer X to Y: ({currentState.JugXVolume + waterToPour}, {currentState.JugYVolume - waterToPour})"
                };
                nextStates.Add(nextState);
            }

            return nextStates;
        }
    }
}
