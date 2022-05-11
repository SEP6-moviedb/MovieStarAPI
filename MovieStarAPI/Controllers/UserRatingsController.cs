using Microsoft.AspNetCore.Mvc;
using MovieStarAPI.Models;

namespace MovieStarAPI.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class UserRatingsController : ControllerBase
        {
            [HttpGet(Name = "GetUserRatings")]
            public async Task<List<Models.UserRating>> GetAsync()
            {
                var ratingList = await RatingService.GetRatings();
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



