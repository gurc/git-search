using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GitSearch.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GitSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FindController : ControllerBase
    {
        ApplicationContext db;
        public FindController()
        {
            db = new ApplicationContext();
        }

        [HttpPost]
        public async Task<ActionResult<List<Repo>>> Post([FromBody]string search)
        {
            return await Utils.Utils.Find(search);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SearchPresentation>>> Get()
        {
            return await db.SearchResults.Select(sr=>new SearchPresentation(sr)).ToListAsync();
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
