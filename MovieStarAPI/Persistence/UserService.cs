using System.Net;

namespace MovieStarAPI.Persistence
{
    public class UserService
    {
        public static async Task<HttpStatusCode> GetUser(string? username, string? password)
        {
            Console.WriteLine("username: " + username + " password: " + password);
            return HttpStatusCode.OK;
            //return HttpStatusCode.NotFound;
        }
    }
}
