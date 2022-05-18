using MovieStarAPI.Models;
using Newtonsoft.Json;

namespace MovieStarAPI.Controllers
{
    public class StatisticsService
    {
        public static async Task<List<ActorStatistics>> GetActorStatistics()
        {
            var httpClient = new HttpClient();
            string apiKey = "d969c038879a912c97bceafc05ec99cd";

            var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.themoviedb.org/3/person/popular?api_key=" + apiKey
                + "&language=en-US&page=1&include_adult=false");

            var response = httpClient.SendAsync(request);

            ActorSearchResult actorSearchResult = JsonConvert.DeserializeObject<ActorSearchResult>(response.Result.Content.ReadAsStringAsync().Result);
            
            List<ActorStatistics> actorStatisticsList = new List<ActorStatistics>();
            
            foreach (var actor in actorSearchResult.results)
            {
                ActorStatistics actorStatistics = new ActorStatistics();

                actorStatistics.actorId = actor.id;
                actorStatistics.actorName = actor.name;
                actorStatistics.popularity = actor.popularity;

                double voteAvgTotal = 0;
                List<KnownFor>? knownForList = actor.known_for;
             
                foreach (var item in knownForList)
                {
                    voteAvgTotal += item.vote_average;
                }

                actorStatistics.voteAverage = voteAvgTotal / knownForList.Count();

                actorStatisticsList.Add(actorStatistics);
            }

            return actorStatisticsList;

        }

        public static async Task<List<MovieStatistics>> GetMovieStatistics()
        {

            List<UserRatingAvg> userRatingAvgList = await RatingService.GetUserRatingsAvg(null);

            userRatingAvgList = userRatingAvgList.OrderBy(x => x.userRatingAvg).Take(20).ToList();

            List<MovieStatistics> movieStatisticsList = new List<MovieStatistics>();
            foreach (var item in userRatingAvgList)
            {
                MovieStatistics movieStatistics = new MovieStatistics();
                
                movieStatistics.movieId = item.movieId;
                movieStatistics.movieUserRatingAvg = item.userRatingAvg;

                
                
                //TODO //Get and attach movie names from tmdb

                string movieName = "Dummy Name " + item.movieId;
                
                movieStatistics.movieName = movieName;



                movieStatisticsList.Add(movieStatistics);
            }

            return movieStatisticsList;

        }
    }
}

