namespace MovieStarAPI.Models
{
    public class Actor
    {
        public string birthday { get; set; }
        public List<string> also_known_as { get; set; }
        public string deathday { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public List<KnownFor> known_for { get; set; }
        public int gender  { get; set; }
        public string biography { get; set; }
        public double popularity { get; set; }
        public string place_of_birth { get; set; }
        public string profile_path { get; set; }
        public bool adult { get; set; }
        public string imdb_id { get; set; }
        public string homepage { get; set; }
        public string ToString()
            
        {
            string knownForStr = "";
            if (known_for != null) { foreach (KnownFor k in known_for) { knownForStr += k + ", \n" ; }; }
            return "birthday: " + birthday + "\nalso_known_as: " + also_known_as + " \ndeathday: " + deathday + "\nid: " + id + 
                   "\nbiography: " + biography + "\nName: " + name + "\nGender: " + gender + "\nPopularity: " + popularity + "\nplace_of_birth: " + 
                   place_of_birth + "\nKnown for: \n" + knownForStr + "\nprofile_path: " + profile_path + "\nadult: " + adult + "\nimdb_id: " + 
                   imdb_id + "\nhomepage: " + homepage + "\n";
        }
    } 
}
