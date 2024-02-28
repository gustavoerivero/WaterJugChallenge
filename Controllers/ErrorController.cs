using Microsoft.AspNetCore.Mvc;
using System.Text;

using WaterJugChallenge.Models;
using WaterJugChallenge.Utils;

namespace WaterJugChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        public ErrorController() { }
        /*
        [HttpPost("Challenge")]
        public async Task<IActionResult> Challenge(Capacities values)
        {
            Response<object> response = new();

            try
            {

                if (values.XCapacity <= 0 || values.YCapacity <= 0 || values.ZTarget <= 0)
                {
                    response.Ok = 0;
                    response.Message = "Jug capacities and target amount should be positive integers.";
                    return BadRequest(response);
                }

                Bucket XWaterJug = new(values.XCapacity);
                Bucket YWaterJug = new(values.YCapacity);

                int targetAmount = values.ZTarget;

                if (!IsTargetAchievable(XWaterJug, YWaterJug, targetAmount))
                {
                    response.Ok = 0;
                    response.Message = "No solution.";
                    return BadRequest(response);
                }

                List<string>? results = SolveWaterJugChallenge(XWaterJug.Capacity, YWaterJug.Capacity, targetAmount);

                if (results == null)
                {
                    response.Ok = 0;
                    response.Message = "No solution.";
                    return BadRequest(response);
                }

                List<object> resultList = new();

                foreach (var result in results)
                {
                    resultList.Add(new
                    {
                        Explanation = result
                    });
                }

                response.Ok = 1;
                response.Message = "Solved.";
                response.Data.Add(new
                {
                    result = resultList,
                    Steps = resultList.Count()
                });

            }
            catch (Exception ex)
            {
                response.Ok = 0;
                response.Message = "No solution. " + ex.Message;
                return BadRequest(response);
            }

            return Ok(response);

        }
        
        private static bool IsTargetAchievable(Bucket jugX, Bucket jugY, int targetAmount)
        {

            if (targetAmount == jugX.Capacity || targetAmount == jugY.Capacity)
            {
                return true;
            }

            if (targetAmount > Math.Max(jugX.Capacity, jugY.Capacity))
            {
                return false;
            }

            int gcd = GCDFinder.GCD(jugX.Capacity, jugY.Capacity, targetAmount);
            return gcd > 1;

        }

        private List<string>? SolverWaterJugChallenge(Bucket jugX, Bucket jugY, int target)
        {
            Queue<State> queue = new();
            List<string> operations = new();

            if (jugX.Capacity - target <= jugY.Capacity - target)
            {
                jugX.Fill();
                operations.Add("Fill X");
            }
            else
            {
                jugY.Fill();
                operations.Add("Fill Y");
            }

            while (jugX.CurrentAmount != target && jugY.CurrentAmount != target)
            {
                



            }

        }

        private List<string>? SolveWaterJugChallenge(int x, int y, int target)
        {
            Queue<State> queue = new();
            queue.Enqueue(new State() { X = 0, Y = 0 });

            HashSet<string> visited = new();

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node.IsTarget(target))
                {
                    return node.Steps;
                }

                JugOperations operations = new();

                List<Operation> nextStates = new()
                {
                    operations.Fill(x, node, "X"),
                    operations.Fill(y, node, "Y"),
                    operations.Empty(node, "X"),
                    operations.Empty(node, "Y"),
                    operations.Transfer(x, y, node, true),
                    operations.Transfer(x, y, node, false)
                };

                foreach (var nextState in nextStates)
                {
                    var stateString = nextState.ToString();

                    if (!visited.Contains(stateString))
                    {
                        visited.Add(stateString);

                        var steps = node.Steps;
                        steps.Add($"{nextState.Action} (X: {nextState.A}, Y: {nextState.B})");

                        queue.Enqueue(new State()
                        {
                            X = nextState.A,
                            Y = nextState.B,
                            Steps = steps
                        });
                    }
                }

            }

            return null;

        }
        */
    }
}
