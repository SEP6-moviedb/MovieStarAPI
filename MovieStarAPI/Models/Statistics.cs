﻿namespace MovieStarAPI.Models
{
    public class ActorStatistics
    {
        public int actorId { get; set; }  //derived from tmdb directly
        public string actorName { get; set; }  //derived from tmdb directly
        public double voteAverage { get; set; } //calculated from tmdb's known_for list
        public double popularity { get; set; } //derived from tmdb directly
        public double avgMovieRating { get; set; } //calculated 


        public string ToString()
        {
            string str =
              "actorId: " + actorId + "\n"
            + " actorName: " + actorName + "\n"
            + " voteAverage: " + voteAverage + "\n"
            + " popularity: " + popularity + "\n\n";

            return str;
        }
    }


    public class MovieStatistics
    {
        public string movieId { get; set; }
        public string movieName { get; set; }
        public double movieUserRatingAvg { get; set; }
        public double movieTmdbRatingAvg { get; set; }

    }

}
