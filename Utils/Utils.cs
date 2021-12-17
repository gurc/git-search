using System.Collections.Generic;
using System.Linq;
using GitSearch.Model;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace GitSearch.Utils
{
    public static class Utils
    {
        private static readonly HttpClient client = new HttpClient();
        private static string token = "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";
        private static readonly ApplicationContext _context;

        static Utils()
        {
            client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);

            _context = new ApplicationContext();
        }
        public static List<Repo> Parse(string json)
        {
            JArray items = (JArray)JObject.Parse(json)["items"];
            var Repos = items.Select(item => new Repo
            {
                Name = (string)item["name"],
                Author = (string)item["owner"]["login"],
                Stargazers = (int)item["stargazers_count"],
                Watchers = (int)item["watchers_count"],
                Url = (string)item["html_url"]
            }).ToList();

            return Repos;
        }

        public static async Task<List<Repo>> Find(string search)
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
                HttpResponseMessage response = await client.GetAsync("https://api.github.com/search/repositories?q=" + search);
                var status = response.StatusCode;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    json = await responseContent.ReadAsStringAsync();

                    _context.Add(new SearchResult { Search = search, Result = json });
                    await _context.SaveChangesAsync();
                }
            }

            if (json != null)
            {
                Repos = Parse(json);
            }

            return Repos;
        }
    }
}
