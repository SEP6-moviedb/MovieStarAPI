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
        public async Task<ContentResult> GetAsync([FromQuery] string? username, string? password)
        {
            ContentResult contentResult = await UserService.GetUser(username, password);

            return contentResult;

        }

        [HttpPost(Name = "PostUser")]
        public async Task<ContentResult> PostAsync([FromQuery] string? username, string? password, string? displayname)
        {
            ContentResult contentResult = await UserService.PostUser(username, password, displayname);
            return contentResult;

        }
    }
}
