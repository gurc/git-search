using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitSearch.Model;
using GitSearch.Services;

namespace GitSearch.Pages
{

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string Search;
        public List<Repo> Repos;
        IFinder _finder;

        public IndexModel(ILogger<IndexModel> logger, IFinder finder)
        {
            _logger = logger;
            _finder = finder;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string search)
        {
            Search = search;

            Repos = await _finder.Find(search);
            
            return Page();
        }
    }
}
