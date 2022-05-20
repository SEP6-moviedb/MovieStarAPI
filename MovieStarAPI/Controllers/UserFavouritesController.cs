using MovieStarAPI.Persistence;

namespace MovieStarAPI.Controllers; 
using Microsoft.AspNetCore.Mvc;
using MovieStarAPI.Models;

[ApiController]
[Route("[controller]")]
public class FavouritesController : ControllerBase {
    
        [HttpGet(Name = "GetUserFavourites")]
        public async Task<List<Models.Favourite>> GetAsync([FromQuery] string? userid)
        {
            var favouriteList = await FavouriteService.CallAPI(userid);
            return favouriteList;
        }

        [HttpPost(Name = "PostFavourite")] //endpoint for both signin and signup
        public void PostAsync([FromBody] Models.Favourite favouriteObject)
        {
            FavouriteService.PostFavourite(favouriteObject);
        }

    }


    
