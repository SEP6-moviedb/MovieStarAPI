using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MovieStarAPI.Models
{

    public class CommentRoot
    {
    public Id _id { get; set; }
    public TimeStamp timeStamp { get; set; }
    public UserComment userComment { get; set; }

}
public class UserComment
    {
        public string movieId { get; set; }
        public string comment { get; set; }
        public string userId { get; set; }
        public int commentId { get; set; }

        public override string ToString()
        {
            return "Movie ID: " + movieId + ", \t Rating: " + comment + ", \t User ID: " + userId + ", \t CommentId: " + commentId;
        }

    }
}
