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
        [HttpGet(Name = "GetUser")]
        public async Task<ContentResult> GetAsync([FromQuery] string? username, string? password)
        {
            ContentResult contentResult = await UserService.GetUser(username, password);

            return contentResult;

        }

        [HttpPost(Name = "PostUser")]
        public ContentResult PostAsync([FromBody] Models.User userObject)
        {
            ContentResult contentResult =  UserService.PostUser(userObject);       
            return contentResult;

        }
    }
}
