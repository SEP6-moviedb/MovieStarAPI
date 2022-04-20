namespace MovieStarAPI.Models
{
    public class Actor
    {
        public bool adult { get; set; }
        public int gender { get; set; }
        public int id { get; set; }
        public List<KnownFor> known_for { get; set; }
        public string known_for_department { get; set; }
        public string name { get; set; }
        public double popularity { get; set; }
        public string profile_path { get; set; }

        public string ToString()
            
        {
            string knownForStr = "";
            if (known_for != null) { foreach (KnownFor k in known_for) { knownForStr += k + ", \n" ; }; }
            return "name: " + name + "\nGender: " + gender + "\nPopularity: " + popularity + " \nKnown for: \n" + knownForStr + "\n";
        }
    } 
}
