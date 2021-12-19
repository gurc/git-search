using Microsoft.AspNetCore.Mvc;
using GitSearch.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GitSearch.Services;

namespace GitSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FindController : ControllerBase
    {
        ApplicationContext db;
        IFinder _finder;
        IParser _parser;
        public FindController(IFinder finder, IParser parser)
        {
            db = new ApplicationContext();
            _finder = finder;
            _parser = parser;
        }

        [HttpPost]
        public async Task<ActionResult<List<Repo>>> Post([FromBody]string search)
        {
            return await _finder.Find(search);
        }

        [HttpGet]
        public async Task<ActionResult<object>> Get()
        {
            return await db.SearchResults.Select(sr=>new {Id=sr.Id, Search=sr.Search, Result=_parser.Parse(sr.Result)}).ToListAsync();
        }
        

        [HttpDelete("{id}")]
        public async Task<ActionResult<SearchResult>> Delete(int id)
        {
            var searhResult = db.SearchResults.FirstOrDefault(x => x.Id == id);
            if (searhResult == null)
            {
                return NotFound();
            }
            db.SearchResults.Remove(searhResult);
            await db.SaveChangesAsync();
            return Ok(searhResult);
        }
    }
}
