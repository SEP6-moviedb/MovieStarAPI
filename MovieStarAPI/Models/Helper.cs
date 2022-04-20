using System.Text;
using System.Web.Mvc;

namespace MovieStarAPI.Models
{
    public class Helper
    {
    }

    public class KnownFor
    {
        public string poster_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public string original_title { get; set; }
        public List<object> genre_ids { get; set; }
        public int id { get; set; }
        public string media_type { get; set; }
        public string original_language { get; set; }
        public string title { get; set; }
        public string backdrop_path { get; set; }
        public double popularity { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public string first_air_date { get; set; }
        public List<string> origin_country { get; set; }
        public string name { get; set; }
        public string original_name { get; set; }
    }

    public class Result
    {
        public string profile_path { get; set; }
        public bool adult { get; set; }
        public int id { get; set; }
        public List<KnownFor> known_for { get; set; }
        public string name { get; set; }
        public double popularity { get; set; }

        public string ToString()
        {
            return "Name: " + this.name + ", " + this.adult + ", " + this.popularity + ", " + this.id;
        }
    }

    public class ResponseSearchPeople
    {
        public int page { get; set; }
        public List<Result> results { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
    }

    public class ResponsePerson
    {
        public bool adult { get; set; }
        public List<string> also_known_as { get; set; }
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
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems /
                    ItemsPerPage);
            }
        }
    }
}
