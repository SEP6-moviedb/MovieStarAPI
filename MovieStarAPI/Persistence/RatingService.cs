using MovieStarAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MongoDB.Bson;
using System.Diagnostics;
using Newtonsoft.Json.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System.Text;

namespace MovieStarAPI.Controllers
{
    public class RatingService
    {
        private static readonly string Secret = "10minute";
        private static readonly string Url = "https://data.mongodb-api.com/app/10minexample-ivjtg/endpoint/ratings?secret="+Secret;

        // GET USER RATINGS
        public static async Task<List<UserRatingAvg>> GetUserRatingsAvg(string? movieid)
        {
            HttpClient httpClient = new HttpClient();
            
            HttpRequestMessage? request = new HttpRequestMessage(new HttpMethod("GET"), 
                Url + (movieid != null ? ("&movieid=" + movieid) : ""));

            Task<HttpResponseMessage>? response = httpClient.SendAsync(request);
            Console.WriteLine("GET request: Status code : " + response.Result.StatusCode);

            JsonWriterSettings jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }; 

            string? ratingsBsonString = response.Result.Content.ReadAsStringAsync().Result;
            BsonArray ratingBsonArray = BsonSerializer.Deserialize<BsonArray>(ratingsBsonString);
         
            List<UserRatingAvg> ratingList = new List<UserRatingAvg>();

            foreach (var ratingBson in ratingBsonArray)
            {
                string? ratingJson = ratingBson.ToJson(jsonWriterSettings);
                UserRatingAvg userRatingAvg = Newtonsoft.Json.JsonConvert.DeserializeObject<UserRatingAvg>(ratingJson);
                ratingList.Add(userRatingAvg);
                Console.WriteLine("userRating: " + userRatingAvg);
            }
            return ratingList;
        }

        // POST RATING
        public static void PostRating(UserRating userRating)
        {
            HttpClient httpClient = new HttpClient();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(userRating);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage>? response = httpClient.PostAsync(Url, data);

            System.Net.HttpStatusCode statusCode = response.Result.StatusCode;
            Console.WriteLine("POST request: Status code: " + statusCode);
        }
    }


}
