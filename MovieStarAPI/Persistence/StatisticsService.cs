using MovieStarAPI.Models;
using Newtonsoft.Json;

namespace MovieStarAPI.Controllers
{
    public sealed class StatisticsService
    {
        private static StatisticsService instance = null;
        private static readonly object padlock = new object();

        private List<MovieStatistics> movieStatisticsList = new List<MovieStatistics>();
        private DateTime lastRefreshTimeStamp = DateTime.Now.AddHours(-10);

        private readonly static int RefreshIntervalHours = 1;
        private readonly static int NumberOfMoviesToReturn = 10;

        StatisticsService()
        {
        }

        public static StatisticsService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new StatisticsService();
                    }
                    return instance;
                }
            }
        }

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

        public async Task<List<MovieStatistics>> GetMovieStatistics()
        {
            if ((DateTime.Now - lastRefreshTimeStamp).TotalHours > RefreshIntervalHours)
            {
                this.movieStatisticsList = await GetRefreshedMovieStatistics();
                lastRefreshTimeStamp = DateTime.Now;
            }
            return movieStatisticsList;
        }

        public async Task<List<MovieStatistics>> GetRefreshedMovieStatistics()
        {
            int numberOfcollectedMovies = 0;
            List<UserRatingAvg> userRatingAvgList = await RatingService.GetUserRatingsAvg(null);

            userRatingAvgList = userRatingAvgList.OrderBy(x => x.userRatingAvg).ToList();

            List<MovieStatistics> movieStatisticsList = new List<MovieStatistics>();

            do
            {
                foreach (var item in userRatingAvgList)
                {
                    MovieStatistics movieStatistics = new MovieStatistics();

                    var httpClient = new HttpClient();
                    string apiKey = "d969c038879a912c97bceafc05ec99cd";

                    // Get request for search movies https://developers.themoviedb.org/3/search/search-movies
                    var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.themoviedb.org/3/movie/" + item.movieId
                        + "?api_key=" + apiKey);

                    var response2 = httpClient.SendAsync(request);

                    Movie movieFromTmdb = JsonConvert.DeserializeObject<Movie>(response2.Result.Content.ReadAsStringAsync().Result);

                    if (movieFromTmdb.title == null)
                    {
                        movieFromTmdb.title = movieFromTmdb.name; //sometimes name is present when title is not
                    }
                    movieStatistics.movieId = item.movieId;
                    movieStatistics.movieUserRatingAvg = item.userRatingAvg;
                    movieStatistics.movieName = movieFromTmdb.title;

                    if (movieStatistics.movieName != null)
                    {
                        movieStatisticsList.Add(movieStatistics);
                        numberOfcollectedMovies += 1;
                    }
                }
            } while (numberOfcollectedMovies < NumberOfMoviesToReturn);

            return movieStatisticsList;
        }
    }
}

