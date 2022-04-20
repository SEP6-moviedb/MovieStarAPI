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

            /*Calling API https://developers.themoviedb.org/3/search/search-people */
            string apiKey = "d969c038879a912c97bceafc05ec99cd";
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/search/person?api_key=" + apiKey + "&language=en-US&query=" + searchText + "&include_adult=false") as HttpWebRequest;
            
            string apiResponse = "";

            HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse;
            
            StreamReader reader = new StreamReader(response.GetResponseStream());
            apiResponse = reader.ReadToEnd();
            
            
            ResponseSearchPeople rootObject = JsonConvert.DeserializeObject<ResponseSearchPeople>(apiResponse);
            
            foreach (Result result in rootObject.results)
            {
                Debug.WriteLine("Result: " + result.ToString());
                Console.WriteLine("Result: " + result.ToString());
            }



            // THIS IS OUR OWN NICE AND FINE RECIPE :)
            var httpClient = new HttpClient();
            // Get request for search movies https://developers.themoviedb.org/3/search/search-movies
            var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.themoviedb.org/3/search/movie?api_key=" + apiKey 
                + "&language=en-US&query=" + searchText + "&page=1&include_adult=false");

            //request.Headers.TryAddWithoutValidation("authorization", "Bearer <<access_token>>");
            //
            //request.Content = new StringContent("{}");
            //request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
            var response2 = httpClient.SendAsync(request);
            Console.WriteLine(response2.Result.StatusCode + "<----statuttrr");
            Console.WriteLine(await response2.Result.Content.ReadAsStringAsync()+ "<----statuttrr");
        }
    }
}
