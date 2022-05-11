using Microsoft.AspNetCore.Mvc;
using MovieStarAPI.Models;

namespace MovieStarAPI.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class UserRatingsController : ControllerBase
        {
            [HttpGet(Name = "GetUserRatingsAvg")]
            public async Task<List<Models.UserRatingAvg>> GetAsync([FromQuery] string? movieid)
            {
                var ratingList = await RatingService.GetUserRatingsAvg(movieid);
                return ratingList;
            }

        [HttpPost(Name = "PostUserRating")]
        public void PostAsync([FromQuery] string movieid, int rating, string userid)
        {
            UserRating userRating = new UserRating(movieid, rating, userid);
            Console.WriteLine("Posting UserRating: " + userRating);
            RatingService.PostRating(userRating);

        }


    }
    }



