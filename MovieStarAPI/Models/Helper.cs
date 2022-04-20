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
            return this.name + this.adult + this.popularity + this.id;
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

    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            string anchorInnerHtml = "";
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                anchorInnerHtml = AnchorInnerHtml(i, pagingInfo);

                if (anchorInnerHtml == "..")
                    tag.MergeAttribute("href", "#");
                else
                    tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = anchorInnerHtml;
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("active");
                }
                tag.AddCssClass("paging");
                if (anchorInnerHtml != "")
                    result.Append(tag.ToString());
            }
            return null;
            //return MvcHtmlString.Create(result.ToString());
        }

        public static string AnchorInnerHtml(int i, PagingInfo pagingInfo)
        {
            string anchorInnerHtml = "";
            if (pagingInfo.TotalPages <= 10)
                anchorInnerHtml = i.ToString();
            else
            {
                if (pagingInfo.CurrentPage <= 5)
                {
                    if ((i <= 8) || (i == pagingInfo.TotalPages))
                        anchorInnerHtml = i.ToString();
                    else if (i == pagingInfo.TotalPages - 1)
                        anchorInnerHtml = "..";
                }
                else if ((pagingInfo.CurrentPage > 5) && (pagingInfo.TotalPages - pagingInfo.CurrentPage >= 5))
                {
                    if ((i == 1) || (i == pagingInfo.TotalPages) || ((pagingInfo.CurrentPage - i >= -3) && (pagingInfo.CurrentPage - i <= 3)))
                        anchorInnerHtml = i.ToString();
                    else if ((i == pagingInfo.CurrentPage - 4) || (i == pagingInfo.CurrentPage + 4))
                        anchorInnerHtml = "..";
                }
                else if (pagingInfo.TotalPages - pagingInfo.CurrentPage < 5)
                {
                    if ((i == 1) || (pagingInfo.TotalPages - i <= 7))
                        anchorInnerHtml = i.ToString();
                    else if (pagingInfo.TotalPages - i == 8)
                        anchorInnerHtml = "..";
                }
            }
            return anchorInnerHtml;
        }
    }
}
