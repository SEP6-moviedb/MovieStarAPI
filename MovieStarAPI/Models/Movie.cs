using Newtonsoft.Json;

namespace MovieStarAPI.Models
{

    public class Movie
    { 
       
            public bool adult { get; set; }
            public string backdrop_path { get; set; }
            public List<int> genre_ids { get; set; }
            public int id { get; set; }
            public string original_language { get; set; }
            public string original_title { get; set; }
            public string overview { get; set; }
            public double popularity { get; set; }
            public string poster_path { get; set; }
            public string release_date { get; set; }
            public string title { get; set; }
            public string name { get; set; }
            public bool video { get; set; }
            public double vote_average { get; set; }
            public int vote_count { get; set; }


        public override string ToString()
        {
            return "Is it porn? " + adult + " title: " + original_title + " Popularity: " + popularity + " Release date: " + release_date +
               " Title: " + title + " video: " + video + " vote avg: " + vote_average + " vote count: " + vote_count;
        }

    }


}
