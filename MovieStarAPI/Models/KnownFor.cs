namespace MovieStarAPI.Models
{
    public class KnownFor
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public List<int> genre_ids { get; set; }
        public int id { get; set; }
        public string media_type { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public string first_air_date { get; set; }
        public string name { get; set; }
        public List<string> origin_country { get; set; }
        public string original_name { get; set; }

        public override string ToString()
        {
            return "Movie title: " + title + " Vote Count: " + vote_count + " average rating: " + vote_average;
        }
    }
}
