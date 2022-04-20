using MovieStarAPI.Models;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace MovieStarAPI.Controllers
{

    public class tmdbController
    {
        public static async void CallAPI(string searchText)
        {
            var httpClient = new HttpClient();
            string apiKey = "d969c038879a912c97bceafc05ec99cd";
            
            // Get request for search movies https://developers.themoviedb.org/3/search/search-movies
            var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.themoviedb.org/3/search/movie?api_key=" + apiKey 
                + "&language=en-US&query=" + searchText + "&page=1&include_adult=false");
            
            var response2 = httpClient.SendAsync(request);
            Console.WriteLine(response2.Result.StatusCode + "<----statuttrr");

            MovieSearchResult movieSearchResult = JsonConvert.DeserializeObject<MovieSearchResult>(response2.Result.Content.ReadAsStringAsync().Result);
            
            foreach (var item in movieSearchResult.results)
            {
                Console.WriteLine(item.ToString());
            }

            // THIS IS OUR OWN NICE AND FINE RECIPE :)
            // Get request for search movies https://developers.themoviedb.org/3/search/search-people
            var request2 = new HttpRequestMessage(new HttpMethod("GET"), "https://api.themoviedb.org/3/search/person?api_key=" + apiKey 
                + "&language=en-US&query=" + searchText + "&page=1&include_adult=false");
            
            var response3 = httpClient.SendAsync(request2);
            Console.WriteLine(response3.Result.StatusCode + "<----statuttrr");

            ActorSearchResult actorSearchResult = JsonConvert.DeserializeObject<ActorSearchResult>(response3.Result.Content.ReadAsStringAsync().Result);
            
            foreach (var item in actorSearchResult.results)
            {
                Console.WriteLine(item.ToString());
            }
            
        }
    }
}
