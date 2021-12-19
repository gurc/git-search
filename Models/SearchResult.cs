namespace GitSearch.Model
{
    public class SearchResult
    {
        public int Id { get; set; }
        public string Search { get; set; }
        public string Result { get; set; }

        public SearchResult(string search, string result)
        {
            Search = search;
            Result = result;
        }
    }
}
