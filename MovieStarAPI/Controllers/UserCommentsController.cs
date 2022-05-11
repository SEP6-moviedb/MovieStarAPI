using Microsoft.AspNetCore.Mvc;

namespace MovieStarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserCommentsController : ControllerBase
    {
        [HttpGet(Name = "GetUserComments")]
        public async Task<List<Models.UserComment>> GetAsync()
        {
            var commentList = await CommentService.CallAPI("leon");
            return commentList;
        }

    }
}
