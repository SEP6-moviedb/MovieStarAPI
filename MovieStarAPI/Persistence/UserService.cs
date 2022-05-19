using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MovieStarAPI.Models;
using System.Net;

namespace MovieStarAPI.Persistence
{
    public class UserService
    {
        private static readonly string Secret = "10minute";
        private static readonly string Url = "https://data.mongodb-api.com/app/10minexample-ivjtg/endpoint/users?secret=" + Secret;

        // GET USER RATINGS
        public static async Task<ContentResult> GetUser(string? username, string? password)
        {
            HttpClient httpClient = new HttpClient();

            HttpRequestMessage? request = new HttpRequestMessage(new HttpMethod("GET"),
                Url + (username != null ? ("&userid=" + username) : "")
                    + (password != null ? ("&password=" + password) : ""));

            Task<HttpResponseMessage>? response = httpClient.SendAsync(request);
            Console.WriteLine("GET request: Status code : " + response.Result.StatusCode);

            JsonWriterSettings jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };

            string? userRootBsonString = response.Result.Content.ReadAsStringAsync().Result;
            BsonArray userRootBsonArray = BsonSerializer.Deserialize<BsonArray>(userRootBsonString);

            User? user = null;

            StatusCodeResult statusCodeResult = new StatusCodeResult(401);

            if (userRootBsonArray.Count != 0) {
                var userRootBson = userRootBsonArray[0];
                string? userRootJson = userRootBson.ToJson(jsonWriterSettings);
                UserRoot? userRoot = Newtonsoft.Json.JsonConvert.DeserializeObject<UserRoot>(userRootJson);
                user = userRoot?.User;

                if (user != null){
                    statusCodeResult = new StatusCodeResult(200);
                }
            }


            string msg = statusCodeResult.StatusCode == 200 ? user.userName : "No user was found with the given credentials";

            Console.WriteLine(msg);

            return new ContentResult() { Content = msg.ToJson(), StatusCode = statusCodeResult.StatusCode };
 

           
        }

        public static Task<ContentResult> PostUser(string? username, string? password, string? displayname)
        {
            //  StatusCodeResult statusCodeResult = new StatusCodeResult(201);
            // return new ContentResult() { Content = msg, StatusCode = statusCodeResult.StatusCode };
            return null;
        }
    }
}
