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
        public ContentResult PostAsync([FromBody] Models.User userObject, [FromQuery] string? action)
        {
            if (action != null || userObject != null) { 
                if (action.Equals("signup")) 
                {
                    Console.WriteLine("Controller: sign UP in progress: " + userObject);
                    return UserService.PostUser(userObject);
                }

                if (action.Equals("signin"))
                {
                    Console.WriteLine("Controller: sign IN in progress: " + userObject);
                    return UserService.GetUser(userObject).Result;
                }
            }

            return new ContentResult() { StatusCode = 404 };
        }
    }
}
