using Microsoft.AspNetCore.Mvc;

using WaterJugChallenge.Models;
using WaterJugChallenge.Utils;

namespace WaterJugChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterJugController : ControllerBase
    {
        /// <summary>
        /// Service to solve the water jug challenge.
        /// </summary>
        /// <param name="values">The values for the maximum capacities of the X jar, Y jar and the Z searched value.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<object> Challenge(Capacities? values)
        {
            // The response model to be returned is instantiated and initialized.
            Response<object> response = new();

            try
            {
                // Validate that all the requested values are being sent.
                // Otherwise, a message is returned indicating that the requested values are required.
                if (values == null || values.XCapacity == null || values.YCapacity == null || values.ZTarget == null)
                {
                    response.Message = "The values of the capabilities and the searched value are required";
                    return BadRequest(response);
                }

                // It is validated that the values received are positive integers.
                // Otherwise, a message is returned indicating that the values passed must be greater than zero.
                if (values.XCapacity <= 0 || values.YCapacity <= 0 || values.ZTarget <= 0)
                {
                    response.Message = "The values of the capacities and the searched value must be integers greater than zero.";
                    return BadRequest(response);
                }

                // It is evaluated whether the past values are valid for solving the water jug challenge.
                // If not, it indicates that there is no solution.
                if (!IsTargetAchievable(values))
                {
                    response.Message = "No solution.";
                    return BadRequest(response);
                }

                // The X jug is instantiated and initialized.
                WaterJug jugX = new()
                {
                    Capacity = (int)values.XCapacity,
                    CurrentAmount = 0
                };

                // The Y jug is instantiated and initialized.
                WaterJug jugY = new()
                {
                    Capacity = (int)values.YCapacity,
                    CurrentAmount = 0
                };

                // Get the solution to the water jug challenge in the form of a list of the steps to reach the solution.
                var waterJugSolution = new WaterJugSolver(jugX, jugY, (int)values.ZTarget).Solve();

                // If the solution has no steps, it indicates that there is no solution.
                if (waterJugSolution.Steps.Count == 0)
                {
                    response.Message = "No solution";
                    return BadRequest(response);
                }

                // A "solved" message is returned with the steps to reach the solution.
                response.Message = "Solved.";
                response.Data = waterJugSolution.Steps;

            }
            catch (Exception ex)
            {
                // The algorithm could not be solved due to an exception, so a "No solution." message is returned along with the exception.
                response.Message = "No solution. " + ex.Message;
                return BadRequest(response);
            }

            return Ok(response);

        }

        /// <summary>
        /// Method that verifies if the searched value Z can be reached with the algorithm.
        /// </summary>
        /// <param name="values">Model that must contain the capacities of the X jar and Y jar, as well as the Z value used.</param>
        /// <returns>True if the value of Z can be reached with the algorithm, false otherwise.</returns>
        private static bool IsTargetAchievable(Capacities values)
        {

            // It is evaluated if the capacity of any of the jugs is exactly equal to the value sought.
            if (values.ZTarget == values.XCapacity || values.ZTarget == values.YCapacity)
            {
                return true;
            }

            // It is evaluated if the value sought is greater than the capacities of the jugs.
            if (values.ZTarget > Math.Max((int)values.XCapacity, (int)values.YCapacity))
            {
                return false;
            }

            // The maximum common divisor between the capacities of the jugs is obtained.
            int gcd = GCDFinder.GCD((int)values.XCapacity, (int)values.YCapacity);

            // Returns true if the remainder between the searched value Z and the greatest common divisor found is exactly equal to zero.
            return values.ZTarget % gcd == 0;

        }

    }
}
