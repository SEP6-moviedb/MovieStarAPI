using Microsoft.AspNetCore.Mvc;

namespace MovieStarAPI.Models
{
    public class Statistics
    {
        public List<KeyValuePair<string, int>> avgMovieRatingsByActor { get; set; }

        public string ToString()
        {
            string str = "Statistics from Statistics.cs: ";

            foreach (var item in avgMovieRatingsByActor)
            {
                str += item + "\n";
            }

            return str;
        }

    };


}
