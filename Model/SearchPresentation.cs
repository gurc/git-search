using System.Collections.Generic;
using System.Linq;

namespace GitSearch.Model
{
    public class SearchPresentation
    {
        public int Id { get; set; }
        public string Search { get; set; }
        public List<Repo> Result { get; set; }
        public SearchPresentation(SearchResult searchResult)
        {
            Id = searchResult.Id;
            Search = searchResult.Search;
            Result = Utils.Utils.Parse(searchResult.Result);
        }
    }
}
