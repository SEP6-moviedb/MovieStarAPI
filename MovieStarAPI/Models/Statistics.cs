using Microsoft.AspNetCore.Mvc;

namespace MovieStarAPI.Models
{
    public class ActorStatistics
    {
        public int actorId {get; set;}  //derived from tmdb directly
        public string actorName { get; set; }  //derived from tmdb directly
        public double voteAverage { get; set; } //calculated from tmdb's known_for list
        public double popularity { get; set; } //derived from tmdb directly


        public string ToString()
        {
            string str = "Statistics from Statistics.cs: \n"
            + "actorId: " + actorId + "\n"
            + " actorName: " + actorName + "\n" 
            + " voteAverage: " + voteAverage + "\n" 
            + " popularity: "  + popularity + "\n\n";
            
            return str;
        }
    }}
