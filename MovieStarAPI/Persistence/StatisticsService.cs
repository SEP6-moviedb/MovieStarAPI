
using MovieStarAPI.Models;

namespace MovieStarAPI.Controllers
{
    public class StatisticsService
    {
        public static async Task<Statistics> GetStatistics() {

            List<KeyValuePair<string, int>> avgMovieRatingsByActor = new List<KeyValuePair<string, int>>();

            avgMovieRatingsByActor.Add(new KeyValuePair<string, int>("actor1", 4));
            avgMovieRatingsByActor.Add(new KeyValuePair<string, int>("actor2", 5));
            avgMovieRatingsByActor.Add(new KeyValuePair<string, int>("actor3", 3));

            Statistics statistics = new Statistics();

            statistics.avgMovieRatingsByActor = avgMovieRatingsByActor;

            return statistics;
        }
    }
}

