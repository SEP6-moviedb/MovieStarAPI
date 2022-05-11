using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MovieStarAPI.Models
{
    public class Id
    {
        [JsonProperty("$oid")]
        public string Oid { get; set; }
    }

    public class RatingRoot
    {
        public Id _id { get; set; }
        public TimeStamp timeStamp { get; set; }
        public UserRating userRating { get; set; }
    }


    public class TimeStamp
    {
        [JsonProperty("$date")]
        public long Date { get; set; }
    }

    public class UserRating
    {
        public string movieId { get; set; }
        public int rating { get; set; }
        public string userId { get; set; }

        public override string ToString()
        {
            return "Movie ID: " + movieId + ", \t Rating: " + rating + ", \t User ID: " + userId;
        }
    }

}


