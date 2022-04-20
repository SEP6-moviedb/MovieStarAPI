using System.ComponentModel.DataAnnotations;

namespace MovieStarAPI.Models
{
    public class TheMovieDb
    {
        [Required]
        public string searchText { get; set; }
        public bool adult { get; set; }
        public string also_known_as { get; set; }
        public string biography { get; set; }
        public string birthday { get; set; }
        public string deathday { get; set; }
        public int gender { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string name { get; set; }
        public string place_of_birth { get; set; }
        public double popularity { get; set; }
        public string profile_path { get; set; }

    }
}
