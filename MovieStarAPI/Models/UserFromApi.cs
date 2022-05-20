using Newtonsoft.Json;

namespace MovieStarAPI.Models
{
    public class UserRoot
    {
        public Id _id { get; set; }

        public Timestamp? timeStamp { get; set; }
        
        public User User { get; set; }
    }

    public class Timestamp
    {
        [JsonProperty("$date")]
        public long Date { get; set; }
    }

    public class User
    {
        public string userName { get; set; }

        public string userId { get; set; }

        public override string ToString()
        {
            return "UserName: " + userName + ", \t UserId: " + userId;
        }
    }
}
