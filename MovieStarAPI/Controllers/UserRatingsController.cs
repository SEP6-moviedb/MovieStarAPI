using Microsoft.AspNetCore.Mvc;

namespace MovieStarAPI.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class UserRatingsController : ControllerBase
        {
            [HttpGet(Name = "GetUserRatings")]
            public async Task<List<Models.UserRating>> GetAsync()
            {
                var ratingList = await RatingService.CallAPI("leonardo");
                return ratingList;
            }

        }
    }

