using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
// d969c038879a912c97bceafc05ec99cd
namespace MovieStarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //omdbTestMethod();
            tmdbController.CallAPI("John");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        /*
        [HttpGet(Name = "GetMovieForecast")]
        public IEnumerable<MovieForecast> GetMovieForecast()
        {
           
            
            return null;
        }

        public void omdbTestMethod() { 
            string apiKey = "4ad124cb-5d8a-48d7-a688-eaab77513e6a";
            string baseUri = $"http://www.omdbapi.com/?apikey={apiKey}";

            string name = "maniac";
            string type = "series";

            var sb = new StringBuilder(baseUri);
            sb.Append($"&s={name}");
            sb.Append($"&type={type}");

            var request = WebRequest.Create(sb.ToString());
            request.Timeout = 1000;
            request.Method = "GET";
            request.ContentType = "application/json";

            string result = string.Empty;

            try
            {
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine(result + "QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQq");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
        */
    }
}