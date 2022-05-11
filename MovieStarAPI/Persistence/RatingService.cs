using MovieStarAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MongoDB.Bson;
using System.Diagnostics;
using Newtonsoft.Json.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace MovieStarAPI.Controllers
{
    public class RatingService
    {
        // GET RATINGS
        public static async Task<List<UserRating>> CallAPI(string searchText)
        {
            HttpClient httpClient = new HttpClient();
            string secret = "10minute";

            HttpRequestMessage? request = new HttpRequestMessage(new HttpMethod("GET"), "https://data.mongodb-api.com/app/10minexample-ivjtg/endpoint/ratings?secret=" + secret);
            Console.WriteLine("Sending request: " + request.RequestUri);

            Task<HttpResponseMessage>? response2 = httpClient.SendAsync(request);
            Console.WriteLine("Status code : " + response2.Result.StatusCode);

            JsonWriterSettings jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }; // key part

            string? ratingsBsonString = response2.Result.Content.ReadAsStringAsync().Result;
            BsonArray ratingBsonArray = BsonSerializer.Deserialize<BsonArray>(ratingsBsonString);

            List<UserRating> ratingList = new List<UserRating>();

            foreach (var ratingBson in ratingBsonArray)
            {
                string? ratingJson = ratingBson.ToJson(jsonWriterSettings);
                RatingRoot root = Newtonsoft.Json.JsonConvert.DeserializeObject<RatingRoot>(ratingJson);
                UserRating userRating = root.userRating;

                ratingList.Add(userRating);
                Console.WriteLine("userRating: " + root.userRating);
            }

            return ratingList;

        }
    }
}
