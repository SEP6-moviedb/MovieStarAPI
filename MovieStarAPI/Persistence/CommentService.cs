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
    public class CommentService
    {
        private static readonly string Secret = "10minute";
        private static readonly string Url = "https://data.mongodb-api.com/app/10minexample-ivjtg/endpoint/comments?secret=" + Secret;

        // GET COMMENTS
        public static async Task<List<Usercomment>> CallAPI(string? movieid)
        {
            HttpClient httpClient = new HttpClient();

            HttpRequestMessage? request = new HttpRequestMessage(new HttpMethod("GET"), Url + (movieid != null ? ("&movieid=" + movieid) : ""));

            Task<HttpResponseMessage>? response = httpClient.SendAsync(request);
            Console.WriteLine("GET request: Status code : " + response.Result.StatusCode);

            JsonWriterSettings jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };

            string? commentsBsonString = response.Result.Content.ReadAsStringAsync().Result;
            BsonArray commentBsonArray = BsonSerializer.Deserialize<BsonArray>(commentsBsonString);

            List<Usercomment> commentList = new List<Usercomment>();

            foreach (var commentBson in commentBsonArray)
            {
                string? commentJson = commentBson.ToJson(jsonWriterSettings);
                Console.WriteLine(commentJson);
                CommentRoot commentRoot = Newtonsoft.Json.JsonConvert.DeserializeObject<CommentRoot>(commentJson);
                commentList.Add(commentRoot.Usercomment);
                Console.WriteLine("userComment: " + commentRoot);
            }

            return commentList;

        }

        // POST COMMENT
        public static void PostComment(Usercomment usercomment)
        {
            HttpClient httpClient = new HttpClient();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(usercomment);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage>? response = httpClient.PostAsync(Url, data);

            System.Net.HttpStatusCode statusCode = response.Result.StatusCode;
            Console.WriteLine("POST request: Status code: " + statusCode);
        }
    }
}
