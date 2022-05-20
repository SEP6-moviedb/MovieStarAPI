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
        [HttpPost(Name = "PostUser")]
        public ContentResult PostAsync([FromBody] Models.User userObject, [FromQuery] string? action)
        {
            if (action != null || userObject != null) { 
                if (action.Equals("signup"))
                    return UserService.PostUser(userObject);

                if (action.Equals("signin"))
                    return UserService.GetUser(userObject).Result;
            }

            return new ContentResult() { StatusCode = 404 };
        }
    }
}
