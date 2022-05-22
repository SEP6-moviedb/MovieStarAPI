using Microsoft.AspNetCore.Mvc;
using MovieStarAPI.Models;
using MovieStarAPI.Persistence;
using System.Net;

namespace MovieStarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpPost(Name = "PostUser")] //endpoint for both signin and signup
        public async Task<ContentResult> PostAsync([FromBody] User userObject, [FromQuery] string? action)
        {
            ContentResult notFound = new ContentResult() { Content = "no valid action parameter was detected", StatusCode = 404 }; //404 Not Found

            if (action != null)
            {
                if (action.Equals("signup"))
                    return await UserService.PostUser(userObject);
                else if (action.Equals("signin"))
                    return await UserService.GetUser(userObject);
                else
                    return notFound;
            }
            else
                return notFound;

        }
    }
}
