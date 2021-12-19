using System.Collections.Generic;
using System.Linq;
using GitSearch.Model;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace GitSearch.Services
{
    public class Finder: IFinder
    {
        private readonly ApplicationContext _context;
        private readonly IClient _client;
        private readonly IParser _parser;

        public Finder(IClient client, IParser parser)
        {
            _context = new ApplicationContext();
            _client = client;
            _parser = parser;
        }

        public async Task<List<Repo>> Find(string search)
        {
            var Repos = new List<Repo>();
            string json = null;

            var searchResult = _context.SearchResults.FirstOrDefault(s => s.Search == search.ToLower());
            if (searchResult != null)
            {
                json = searchResult.Result;
            }
            else
            {
                HttpResponseMessage response = await _client.GetAsync("https://api.github.com/search/repositories?q=" + search);
                var status = response.StatusCode;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    json = await responseContent.ReadAsStringAsync();

                    _context.Add(new SearchResult(search, json));
                    await _context.SaveChangesAsync();
                }
            }

            if (json != null)
            {
                Repos = _parser.Parse(json);
            }

            return Repos;
        }
    }
}
