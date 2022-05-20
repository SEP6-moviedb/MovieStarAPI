using System.Diagnostics;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MovieStarAPI.Models;

namespace MovieStarAPI.Persistence; 

public class FavouriteService {
    
    private static readonly string Secret = "10minute";
        private static readonly string Url = "https://data.mongodb-api.com/app/10minexample-ivjtg/endpoint/favourites?secret=" + Secret;

        // GET FAVOURITES
        public static async Task<List<Favourite>> CallAPI(string? userid)
        {
            HttpClient httpClient = new HttpClient();
        //  https://data.mongodb-api.com/app/10minexample-ivjtg/endpoint/favourites?userid=anders@email.dk
        HttpRequestMessage? request = new HttpRequestMessage(new HttpMethod("GET"), Url + (userid != null ? ("&userid=" + userid) : ""));

            Task<HttpResponseMessage>? response = httpClient.SendAsync(request);
            Console.WriteLine("GET request: Status code : " + response.Result.StatusCode);

            JsonWriterSettings jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
            
            string? favouritesBsonString = response.Result.Content.ReadAsStringAsync().Result;
            BsonArray favouriteBsonArray = BsonSerializer.Deserialize<BsonArray>(favouritesBsonString);
        Console.WriteLine(favouritesBsonString + "QQQQQQQQQQQQQQQQQQQQQQQQQQQQQ");
            List<Favourite> favouriteList = new List<Favourite>();

            foreach (var favouriteBson in favouriteBsonArray)
            {
                string? favouriteJson = favouriteBson.ToJson(jsonWriterSettings);
                Console.WriteLine(favouriteJson + "hahahadhadhadhahda");
                FavouriteRoot favouriteRoot = Newtonsoft.Json.JsonConvert.DeserializeObject<FavouriteRoot>(favouriteJson);
                favouriteList.Add(favouriteRoot.favourite);
                Console.WriteLine("userFavourite: " + favouriteRoot);
            }

            return favouriteList;

        }

        // POST FAVOURITE
        public static void PostFavourite(Favourite userFavourite)
        {
            HttpClient httpClient = new HttpClient();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(userFavourite);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage>? response = httpClient.PostAsync(Url, data);

            System.Net.HttpStatusCode statusCode = response.Result.StatusCode;
            Console.WriteLine("POST request: Status code: " + statusCode);
        }
    }
