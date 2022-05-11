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
    public class CommentService
    {
        // GET COMMENTS
        public static async Task<List<UserComment>> CallAPI(string searchText)
        {
            HttpClient httpClient = new HttpClient();
            string secret = "10minute";

            HttpRequestMessage? request = new HttpRequestMessage(new HttpMethod("GET"), "https://data.mongodb-api.com/app/10minexample-ivjtg/endpoint/comments?secret=" + secret);
            Console.WriteLine("Sending request: " + request.RequestUri);

            Task<HttpResponseMessage>? response2 = httpClient.SendAsync(request);
            Console.WriteLine("Status code : " + response2.Result.StatusCode);

            JsonWriterSettings jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }; // key part


            string? commentsBsonString = response2.Result.Content.ReadAsStringAsync().Result;
            BsonArray commentBsonArray = BsonSerializer.Deserialize<BsonArray>(commentsBsonString);

            List<UserComment> commentList = new List<UserComment>();

            foreach (var commentBson in commentBsonArray)
            {
                string? commentJson = commentBson.ToJson(jsonWriterSettings);
                CommentRoot root = Newtonsoft.Json.JsonConvert.DeserializeObject<CommentRoot>(commentJson);
                UserComment userComment = root.userComment;

                commentList.Add(userComment);
                Console.WriteLine("userComment: " + root.userComment);
            }

            return commentList;

        }
    }
}
