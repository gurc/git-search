using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using GitSearch.Model;
using GitSearch.Utils;

namespace GitSearch.Pages
{

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string Search;
        public List<Repo> Repos;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string search)
        {
            Search = search;

            Repos = await Utils.Utils.Find(search);
            
            return Page();
        }
    }
}
