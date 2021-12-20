using Microsoft.AspNetCore.Mvc;
using GitSearch.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GitSearch.Services;
using Microsoft.Extensions.Configuration;

namespace GitSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FindController : ControllerBase
    {
        ApplicationContext db;
        IFinder _finder;
        IParser _parser;
        int ResultsOnPage;
        public FindController(IFinder finder, IParser parser, IConfiguration configuration)
        {
            db = new ApplicationContext();
            _finder = finder;
            _parser = parser;
            ResultsOnPage = configuration.GetValue<int>("ResultsOnPage");
        }

        [HttpPost]
        public async Task<ActionResult<List<Repo>>> Post([FromBody]string search)
        {
            return await _finder.Find(search);
        }

        [HttpGet]
        [HttpGet("{page}")]
        public async Task<ActionResult<object>> Get(int? page)
        {
            int p = 1;
            if (page != null)
            {
                p = (int)page;
            }

            return await db.SearchResults
                .Skip((p-1) * ResultsOnPage)
                .Take(ResultsOnPage)
                .Select(sr=>new {Id=sr.Id, Search=sr.Search, Result=_parser.Parse(sr.Result)}).ToListAsync();
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
