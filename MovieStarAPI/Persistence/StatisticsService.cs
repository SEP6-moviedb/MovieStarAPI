using MovieStarAPI.Models;
using Newtonsoft.Json;

namespace MovieStarAPI.Controllers
{
    public sealed class StatisticsService
    {
        private static StatisticsService instance = null;
        private static readonly object padlock = new object();

        private List<MovieStatistics>? movieStatisticsList;
        private DateTime lastRefreshTimeStamp = DateTime.Now.AddHours(-10);

        private readonly static int RefreshIntervalHours = 1;
        private readonly static int NumberOfMoviesToReturn = 10;

        private readonly static string ApiKeyParameter = "?api_key=d969c038879a912c97bceafc05ec99cd";
        private readonly static string BaseUrl = "https://api.themoviedb.org/3/";

        public static StatisticsService Instance{
            get{
                lock (padlock){
                    if (instance == null)
                        instance = new StatisticsService();
                    
                    return instance;
                }
            }
        }

        public static async Task<List<ActorStatistics>> GetActorStatistics(){
            string requestUrl = BaseUrl + "person/popular" + ApiKeyParameter + "&language=en-US&page=1&include_adult=false";
            Task<HttpResponseMessage> response = GetResponseFromApi(requestUrl);

            ActorSearchResult actorSearchResult = JsonConvert.DeserializeObject<ActorSearchResult>(response.Result.Content.ReadAsStringAsync().Result);

            List<ActorStatistics> actorStatisticsList = new List<ActorStatistics>();

            foreach (var actor in actorSearchResult.results){
                ActorStatistics actorStatistics = new ActorStatistics(){ 
                actorId = actor.id,
                actorName = actor.name,
                popularity = actor.popularity
                };

            double voteAvgTotal = 0;
            List<KnownFor>? knownForList = actor.known_for;

            foreach (var item in knownForList)
                voteAvgTotal += item.vote_average;

            actorStatistics.voteAverage = voteAvgTotal / knownForList.Count();
            actorStatisticsList.Add(actorStatistics);
            }

            return actorStatisticsList;
        }

        public async Task<List<MovieStatistics>> GetMovieStatistics(){
            if ((DateTime.Now - lastRefreshTimeStamp).TotalHours > RefreshIntervalHours){
                this.movieStatisticsList = await GetRefreshedMovieStatistics();
                lastRefreshTimeStamp = DateTime.Now;
            }

            return movieStatisticsList;
        }

        public async Task<List<MovieStatistics>> GetRefreshedMovieStatistics(){
            List<MovieStatistics> movieStatisticsList = new List<MovieStatistics>();
            int numberOfcollectedMovies = 0;

            // Get list of UserRatingAvg from MongoDB
            List<UserRatingAvg> userRatingAvgList = await RatingService.GetUserRatingsAvg(null);
            userRatingAvgList = userRatingAvgList.OrderBy(x => x.userRatingAvg).ToList();

            do{
                // For each UserRatingAvg call TMDB Api to get movie title 
                foreach (var userRatingAvg in userRatingAvgList){

                    string url = BaseUrl + "movie/" + userRatingAvg.movieId + ApiKeyParameter;
                    Task<HttpResponseMessage> response = GetResponseFromApi(url);

                    Movie movieFromTmdb = JsonConvert.DeserializeObject<Movie>(response.Result.Content.ReadAsStringAsync().Result);

                    if (movieFromTmdb.title == null)
                        movieFromTmdb.title = movieFromTmdb.name; //sometimes name is present when title is not

                    if (movieFromTmdb.title != null && movieFromTmdb.vote_average != 0){

                        MovieStatistics movieStatistics = new MovieStatistics{
                            movieId = userRatingAvg.movieId,
                            movieName = movieFromTmdb.title,
                            movieUserRatingAvg = userRatingAvg.userRatingAvg,
                            movieTmdbRatingAvg = movieFromTmdb.vote_average
                        };

                        movieStatisticsList.Add(movieStatistics);
                        numberOfcollectedMovies++;
                    }
                }
            } while (numberOfcollectedMovies < NumberOfMoviesToReturn);

            return movieStatisticsList;
        }

        private static Task<HttpResponseMessage> GetResponseFromApi(string url){
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("GET"), url);
            var response = httpClient.SendAsync(request);
            return response;
        }
    }
}

