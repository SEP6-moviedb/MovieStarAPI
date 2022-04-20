using MovieStarAPI.Models;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace MovieStarAPI.Controllers
{

    public class tmdbController
    {
        public static void CallAPI(string searchText)
        {
            /*Calling API https://developers.themoviedb.org/3/search/search-people */
            string apiKey = "d969c038879a912c97bceafc05ec99cd";
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/search/person?api_key=" + apiKey + "&language=en-US&query=" + searchText + "&include_adult=false") as HttpWebRequest;

            string apiResponse = "";

            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }
            
            ResponseSearchPeople rootObject = JsonConvert.DeserializeObject<ResponseSearchPeople>(apiResponse);
            
            foreach (Result result in rootObject.results)
            {

                Console.WriteLine("___QQQWW___" + result);
                Debug.WriteLine("__result type___" + result.GetType);
                Debug.WriteLine("___QQQWW___" + result.ToString());
                Debug.WriteLine("___QQQWW___" + result);
                Debug.WriteLine("___QQQWWprofi___" + result.profile_path);
                Debug.WriteLine("___QQQWW___" + result.GetType);
            }
        }
        /*
        public ActionResult GetPerson(int id)
        {
           
            Calling API https://developers.themoviedb.org/3/people
            string apiKey = "d969c038879a912c97bceafc05ec99cd";
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/person/" + id + "?api_key=" + apiKey + "&language=en-US") as HttpWebRequest;

            string apiResponse = "";
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }
            /*End*/

            /*http://json2csharp.com
            ResponsePerson rootObject = JsonConvert.DeserializeObject<ResponsePerson>(apiResponse);
            TheMovieDb theMovieDb = new TheMovieDb();
            theMovieDb.name = rootObject.name;
            theMovieDb.biography = rootObject.biography;
            theMovieDb.birthday = rootObject.birthday;
            theMovieDb.place_of_birth = rootObject.place_of_birth;
            theMovieDb.profile_path = rootObject.profile_path == null ? Url.Content("~/Content/Image/no-image.png") : "https://image.tmdb.org/t/p/w500/" + rootObject.profile_path;
            theMovieDb.also_known_as = string.Join(", ", rootObject.also_known_as);

            return View(theMovieDb);
            }
        */
        
    }


}
