using GitSearch.Model;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace GitSearch.Services
{
    public class Parser: IParser
    {
        public List<Repo> Parse(string json)
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
    }
}
