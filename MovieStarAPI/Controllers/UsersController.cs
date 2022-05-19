using Microsoft.AspNetCore.Mvc;
using MovieStarAPI.Persistence;
using System.Net;

namespace MovieStarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet(Name = "GetUser")]
        public async Task<StatusCodeResult> GetAsync([FromQuery] string? username, string? password)
        {
            StatusCodeResult statusCodeResult = await UserService.GetUser(username, password);

            return statusCodeResult;

                    
        }
    }
}
