namespace MovieStarAPI.Models; 

public class FavouriteRoot {
    public string _id { get; set; }
    public Favourite favourite { get; set; }


}
public class Favourite
{

    public string movieid { get; set; }
    public string userid { get; set; }
    public string moviename { get; set; }

    public Favourite(string movieId, string userid, string moviename)
    {
            
        this.movieid = movieId;
        this.userid = userid;
        this.moviename = moviename;

    }

    public override string ToString()
    {
        return "Movie ID: " + movieid + ", \t User ID: " + userid + ", \t Movie name: " + moviename;
    }
}
