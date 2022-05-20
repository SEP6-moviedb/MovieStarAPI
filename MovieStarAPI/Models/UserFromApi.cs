using Newtonsoft.Json;

namespace MovieStarAPI.Models
{
    public class UserRoot
    {
        public Id _id { get; set; }

        public Timestamp timeStamp { get; set; }
        
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
        public string? displayName { get; set; }
        public string password { get; set; }

       
        public string? userId { get; set; }

        public override string ToString()
        {
            return "UserName: " + userName + ", \t UserId: " + userId + "\t pw: " + password;
        }
    }


    public class MongoDBUser
    {
        public string userName { get; set; }
        public string? userId { get; set; }
        public string password { get; set; }

        public MongoDBUser(User user)
        {
            this.userName = user.displayName;
            this.userId = user.userName;
            this.password = user.password;
        }

        public override string ToString()
        {
            return "UserName: " + userName + ", \t UserId: " + userId + "\t pw: " + password;
        }
    }
}
