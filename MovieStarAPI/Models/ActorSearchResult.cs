namespace MovieStarAPI.Models
{
    public class ActorSearchResult
    {
        public int page { get; set; }
        public List<Actor> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
