using System.Threading.Tasks;
using GitSearch.Model;
using System.Collections.Generic;

namespace GitSearch.Services
{
    public interface IFinder
    {
        public Task<List<Repo>> Find(string search);
    }
}
