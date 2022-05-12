using Microsoft.AspNetCore.Mvc;
using MovieStarAPI.Models;

namespace MovieStarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserCommentsController : ControllerBase
    {
        [HttpGet(Name = "GetUserComments")]
        public async Task<List<Models.Usercomment>> GetAsync([FromQuery] string? movieid)
        {
            var commentList = await CommentService.CallAPI(movieid);
            return commentList;
        }

        [HttpPost(Name = "PostUserComments")]
        public void PostAsync([FromQuery] string movieid, string comment, string username)
        {
            Usercomment userComment = new Usercomment(movieid, comment, username);
            Console.WriteLine("Posting UserComment: " + userComment);
            CommentService.PostComment(userComment);

        }

    }
}
