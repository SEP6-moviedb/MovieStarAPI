using System.Net;

namespace MovieStarAPI.Persistence
{
    public class UserService
    {
        public static async Task<HttpStatusCode> GetUser(string? username, string? password)
        {
            Console.WriteLine("username: " + username + " password: " + password);

            var userPasswordDictionary = new Dictionary<string, string>();

            userPasswordDictionary.Add("anders@email.dk", "anders1234");
            userPasswordDictionary.Add("bo@email.dk", "bo1234");
            userPasswordDictionary.Add("carl@email.dk", "carl1234");

            string found; 
            if (userPasswordDictionary.TryGetValue(username, out found) && found == password)
            {
                // key/value pair exists
                return HttpStatusCode.OK;
            }
                return HttpStatusCode.NotFound;
        }
    }
}
