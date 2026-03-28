
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Task_Manager.Controllers
{
    public class BaseController : ControllerBase
    {

        protected IActionResult HandleResult<T>(T result) where T : class
        {
            dynamic res = result;
            if (res.Success == true)
            {
                return Ok(result);
            }
            else { return BadRequest(result); }


        }


        protected int GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User not authenticated");
            }
            return int.Parse(userId.Value);




        }
    }
}
