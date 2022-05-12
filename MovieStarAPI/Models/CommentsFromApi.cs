using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MovieStarAPI.Models
{

    public class CommentRoot
    {
    public Id _id { get; set; }
    public TimeStamp timeStamp { get; set; }
    public Usercomment Usercomment { get; set; }
    public int commentid { get; set; }

    }
    public class Usercomment
    {
        
        public string movieid { get; set; }
        public string comment { get; set; }
        public string username { get; set; }

        public Usercomment(string movieId, string comment, string username)
        {
            
            this.movieid = movieId;
            this.comment = comment;
            this.username = username;

        }

        public override string ToString()
        {
            return "Movie ID: " + movieid + ", \t Comment: " + comment + ", \t User name: " + username;
        }
    }
 
}
