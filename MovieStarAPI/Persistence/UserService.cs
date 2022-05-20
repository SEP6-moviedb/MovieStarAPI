using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MovieStarAPI.Models;
using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace MovieStarAPI.Persistence
{
    public class UserService
    {
        private static readonly string Secret = "10minute";
        private static readonly string Url = "https://data.mongodb-api.com/app/10minexample-ivjtg/endpoint/users?secret=" + Secret;

        // GET USER
        public static async Task<ContentResult> GetUser(User? userObject)
        {
            HttpClient httpClient = new HttpClient();

            HttpRequestMessage? request = new HttpRequestMessage(new HttpMethod("GET"),
                Url + (userObject != null ? ("&userid=" + userObject.userName + "&password=" + userObject.password) : ""));

            Task<HttpResponseMessage>? response = httpClient.SendAsync(request);
            Console.WriteLine("GET request: Status code : " + response.Result.StatusCode);

            JsonWriterSettings jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };

            string? userRootBsonString = response.Result.Content.ReadAsStringAsync().Result;
            BsonArray userRootBsonArray = BsonSerializer.Deserialize<BsonArray>(userRootBsonString);

            User? user = null;

            StatusCodeResult statusCodeResult = new StatusCodeResult(401);

            if (userRootBsonArray.Count != 0) {
                BsonValue? userRootBson = userRootBsonArray[0];
                string? userRootJson = userRootBson.ToJson(jsonWriterSettings);
                //Console.WriteLine(userRootJson);
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

        // POST USER
        public static ContentResult PostUser(User? userObject)
        {
            Console.WriteLine(userObject + "<---------- userobject");

            //[FromQuery] string? username, string? password, string? displayname

            HttpClient httpClient = new HttpClient();
            MongoDBUser mongoDBuser = new MongoDBUser(userObject);

            Console.WriteLine("Service: sign UP in progress: " + mongoDBuser);
            string? json = Newtonsoft.Json.JsonConvert.SerializeObject(mongoDBuser);
            Console.WriteLine(json + "<---------- json from userservice");

            StringContent? data = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine(json + "<---------- data from userservice");
            Task<HttpResponseMessage>? response = httpClient.PostAsync(Url, data);

            System.Net.HttpStatusCode statusCode = response.Result.StatusCode;
            Console.WriteLine("User POST request: Status code: " + statusCode);

            return new ContentResult() { StatusCode = (int?)statusCode };
        }
    }
}
