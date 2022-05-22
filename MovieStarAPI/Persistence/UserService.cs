using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MovieStarAPI.Models;
using Newtonsoft.Json.Linq;
using System.Net;
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

            JsonWriterSettings jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };

            string? userRootBsonString = response.Result.Content.ReadAsStringAsync().Result;
            BsonArray userRootBsonArray = BsonSerializer.Deserialize<BsonArray>(userRootBsonString);

            User? user = null;

            StatusCodeResult statusCodeResult = new StatusCodeResult(401);

            if (userRootBsonArray.Count != 0)
            {
                BsonValue? userRootBson = userRootBsonArray[0];
                string? userRootJson = userRootBson.ToJson(jsonWriterSettings);
                //Console.WriteLine(userRootJson);
                UserRoot? userRoot = Newtonsoft.Json.JsonConvert.DeserializeObject<UserRoot>(userRootJson);
                user = userRoot?.User;

                if (user != null)
                {
                    statusCodeResult = new StatusCodeResult(200);
                }
            }


            string msg = statusCodeResult.StatusCode == 200 ? user.userName : "No user was found with the given credentials";

            Console.WriteLine(msg);

            return new ContentResult() { Content = msg.ToJson(), StatusCode = statusCodeResult.StatusCode };



        }


        // POST USER
        public async static Task<ContentResult> PostUser(User? userObject)
        {
            HttpClient httpClient = new HttpClient();
            MongoDBUser mongoDBuser = new MongoDBUser(userObject);
            string? json = Newtonsoft.Json.JsonConvert.SerializeObject(mongoDBuser);
            StringContent? data = new StringContent(json, Encoding.UTF8, "application/json");


            HttpResponseMessage? response = await httpClient.PostAsync(Url, data);

            var statusCode = HttpStatusCode.InternalServerError; //500 Internal Server Error
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string? responseString = response.Content.ReadAsStringAsync().Result;
                JObject? responseJobject = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
                JToken? upsertedIdJToken = responseJobject["upsertedId"];

                if (upsertedIdJToken != null)
                    statusCode = HttpStatusCode.Created; //201 Created
                else
                {
                    int matchedCount = int.Parse(responseJobject["matchedCount"].ToString());
                    statusCode = matchedCount > 0 ? HttpStatusCode.Conflict : HttpStatusCode.UnprocessableEntity; //409 Conflict //422 Unprocessable Entity 
                }
            }
            return new ContentResult() { StatusCode = ((int)statusCode) };
        }
    }
}
