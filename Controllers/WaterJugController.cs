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

    }
}
