using GitSearch.Model;
using System.Collections.Generic;

namespace GitSearch.Services
{
    public interface IParser
    {
        public List<Repo> Parse(string json);
    }
}
