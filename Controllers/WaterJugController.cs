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

                WaterJug jugX = new()
                {
                    Capacity = values.XCapacity,
                    CurrentAmount = 0
                };

                WaterJug jugY = new()
                {
                    Capacity = values.YCapacity,
                    CurrentAmount = 0
                };

                var waterJugSolution = new WaterJugSolver(jugX, jugY, values.ZTarget).Solve();

                if (waterJugSolution.Steps.Count == 0)
                {
                    response.Message = "No solution";
                    return BadRequest(response);
                }

                response.Message = "Solved.";
                response.Data = waterJugSolution.Steps;

            }
            catch (Exception ex)
            {
                response.Message = "No solution. " + ex.Message;
                return BadRequest(response);
            }

            return Ok(response);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
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

    }
}
