using Microsoft.AspNetCore.Mvc;
using System.Text;

using WaterJugChallenge.Models;
using WaterJugChallenge.Utils;

namespace WaterJugChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterJugController : ControllerBase
    {
        [HttpPost]
        public ActionResult<object> Challenge(Capacities values)
        {

            Response<object> response = new();

            try
            {
                if (values.XCapacity <= 0 || values.YCapacity <= 0 || values.ZTarget <= 0)
                {
                    response.Message = "The values of the capabilities and the searched value must be greater than zero.";
                    return BadRequest(response);
                }

                if (!IsTargetAchievable(values))
                {
                    response.Message = "No solution.";
                    return BadRequest(response);
                }

                WaterJug bucketX = new()
                {
                    Capacity = values.XCapacity,
                    CurrentAmount = 0
                };

                WaterJug bucketY = new()
                {
                    Capacity = values.YCapacity,
                    CurrentAmount = 0
                };

                response.Message = "Solved.";
                response.Data = SolverWaterJugChallenge(bucketX, bucketY, values.ZTarget);
            }
            catch (Exception ex)
            {
                response.Message = "No solution. " + ex.Message;
                return BadRequest(response);
            }

            return Ok(response);

        }

        private static bool IsTargetAchievable(Capacities values)
        {

            if (values.ZTarget == values.XCapacity || values.ZTarget == values.YCapacity)
            {
                return true;
            }

            if (values.ZTarget > Math.Max(values.XCapacity, values.YCapacity))
            {
                return false;
            }

            int gcd = GCDFinder.GCD(values.XCapacity, values.YCapacity);
            return values.ZTarget % gcd == 0;

        }

        private static List<Operation> SolverWaterJugChallenge(WaterJug jugX, WaterJug jugY, int target)
        {
            
            List<Operation> operations = new();
            int auxTarget = 0;

            while (auxTarget != target)
            {

                if (Math.Abs(jugX.Capacity - target) <= Math.Abs(jugY.Capacity - target))
                {

                    if (jugX.CurrentAmount == 0)
                    {
                        jugX.Fill();
                        operations.Add(new Operation
                        {
                            JugXAmount = jugX.CurrentAmount,
                            JugYAmount = jugY.CurrentAmount,
                            Action = $"Fill X: ({jugX.CurrentAmount}, {jugY.CurrentAmount})"
                        });
                    }
                    else if (jugX.CurrentAmount <= jugX.Capacity)
                    {
                        if (jugY.CurrentAmount != jugY.Capacity)
                        {
                            jugY = jugX.Transfer(jugY);
                            operations.Add(new Operation
                            {
                                JugXAmount = jugX.CurrentAmount,
                                JugYAmount = jugY.CurrentAmount,
                                Action = $"Transfer X to Y: ({jugX.CurrentAmount}, {jugY.CurrentAmount})"
                            });
                        }
                        else
                        {
                            jugY.Empty();
                            operations.Add(new Operation
                            {
                                JugXAmount = jugX.CurrentAmount,
                                JugYAmount = jugY.CurrentAmount,
                                Action = $"Empty Y: ({jugX.CurrentAmount}, {jugY.CurrentAmount})"
                            });
                        }
                    }

                }
                else
                {
                    if (jugY.CurrentAmount == 0)
                    {
                        jugY.Fill();
                        operations.Add(new Operation
                        {
                            JugXAmount = jugX.CurrentAmount,
                            JugYAmount = jugY.CurrentAmount,
                            Action = $"Fill Y: ({jugX.CurrentAmount}, {jugY.CurrentAmount})"
                        });
                    }
                    else if (jugY.CurrentAmount <= jugY.Capacity)
                    {
                        if (jugX.CurrentAmount != jugX.Capacity)
                        {
                            jugX = jugY.Transfer(jugX);
                            operations.Add(new Operation
                            {
                                JugXAmount = jugX.CurrentAmount,
                                JugYAmount = jugY.CurrentAmount,
                                Action = $"Transfer Y to X: ({jugX.CurrentAmount}, {jugY.CurrentAmount})"
                            });
                        }
                        else
                        {
                            jugX.Empty();
                            operations.Add(new Operation
                            {
                                JugXAmount = jugX.CurrentAmount,
                                JugYAmount = jugY.CurrentAmount,
                                Action = $"Empty X: ({jugX.CurrentAmount}, {jugY.CurrentAmount})"
                            });
                        }
                    }

                }

                auxTarget = Math.Abs(jugX.CurrentAmount - target) <= Math.Abs(jugY.CurrentAmount - target) ? jugX.CurrentAmount : jugY.CurrentAmount;

            }

            return operations;

        }

        private List<string> SolveWaterJugChallenge(WaterJug jugX, WaterJug jugY, int target)
        {
            Queue<State> queue = new();
            HashSet<State> visited = new();

            queue.Enqueue(new State(jugX, jugY));
            visited.Add(new State(jugX, jugY));

            while (queue.Count > 0)
            {
                State current = queue.Dequeue();

                if (current.JugX.CurrentAmount == target || current.JugY.CurrentAmount == target)
                {
                    return ReconstructPath(current, visited);
                }

                var nextStates = GetValidNextStates(current);

                foreach (State nextState in nextStates)
                {
                    if (!visited.Contains(nextState))
                    {
                        queue.Enqueue(nextState);
                        visited.Add(nextState);
                    }
                }
            }

            throw new Exception("No solution found."); // If queue is empty
        }

        private List<string> ReconstructPath(State state, HashSet<State> visited)
        {
            List<string> path = new();
            State current = state;

            while (current != null)
            {
                path.Insert(0, current.GetOperationDescription());
                current = visited.FirstOrDefault(s => s.Equals(current.Parent));
            }

            return path;
        }

        private IEnumerable<State> GetValidNextStates(State state)
        {
            List<State> nextStates = new();

            // Fill X
            if (state.JugX.CurrentAmount < state.JugX.Capacity)
            {
                WaterJug jugX = state.JugX;
                jugX.Fill();

                nextStates.Add(new State(jugX, state.JugY));
            }

            // Fill Y
            if (state.JugY.CurrentAmount < state.JugY.Capacity)
            {
                WaterJug jugY = state.JugY;
                jugY.Fill();

                nextStates.Add(new State(state.JugX, jugY));
            }

            // Empty X
            if (state.JugX.CurrentAmount > 0)
            {
                WaterJug jugX = state.JugX;
                jugX.Empty();

                nextStates.Add(new State(jugX, state.JugY));
            }

            // Empty Y
            if (state.JugY.CurrentAmount > 0)
            {
                WaterJug jugY = state.JugY;
                jugY.Empty();

                nextStates.Add(new State(state.JugX, jugY));
            }

            // Transfer X to Y
            if (state.JugX.CurrentAmount > 0 && state.JugY.CurrentAmount < state.JugY.Capacity)
            {
                WaterJug jugX = state.JugX;
                WaterJug jugY = state.JugY;

                nextStates.Add(new State(jugX, jugX.Transfer(jugY)));
            }

            // Transfer Y to X
            if (state.JugY.CurrentAmount > 0 && state.JugX.CurrentAmount < state.JugX.Capacity)
            {
                WaterJug jugX = state.JugX;
                WaterJug jugY = state.JugY;

                nextStates.Add(new State(jugY.Transfer(jugX), jugY)); // Transfer Y to X
            }

            return nextStates;
        }

    }
}
