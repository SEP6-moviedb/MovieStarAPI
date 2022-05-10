using Microsoft.AspNetCore.Mvc;

namespace MovieStarAPI.Controllers
{
    public class MovieStarController
    {
        public static async void CallAPI(string searchText)
        {
            var httpClient = new HttpClient();
            string secret = "10minute";

            // GET RATINGS
            var request = new HttpRequestMessage(new HttpMethod("GET"), "https://data.mongodb-api.com/app/10minexample-ivjtg/endpoint/ratings?secret=");

            var response2 = httpClient.SendAsync(request);
            Console.WriteLine(response2.Result.StatusCode + "<----status from moviestarcontroller");
            Console.WriteLine(response2.Result.Content.ReadAsStringAsync().Result + "<---- ressoures from moviestarcontroller");

            /*MovieSearchResult movieSearchResult = JsonConvert.DeserializeObject<MovieSearchResult>(response2.Result.Content.ReadAsStringAsync().Result);

            foreach (var item in movieSearchResult.results)
            {
                Console.WriteLine(item.ToString());
            }

            // THIS IS OUR OWN NICE AND FINE RECIPE :)
            // Get request for search movies https://developers.themoviedb.org/3/search/search-people
            var request2 = new HttpRequestMessage(new HttpMethod("GET"), "https://api.themoviedb.org/3/search/person?api_key=" + secret
                + "&language=en-US&query=" + searchText + "&page=1&include_adult=false");

            var response3 = httpClient.SendAsync(request2);
            Console.WriteLine(response3.Result.StatusCode + "<----statuttrr");

            ActorSearchResult actorSearchResult = JsonConvert.DeserializeObject<ActorSearchResult>(response3.Result.Content.ReadAsStringAsync().Result);

            foreach (var item in actorSearchResult.results)
            {
                Console.WriteLine(item.ToString());
            }
            */

        }
    }
}
