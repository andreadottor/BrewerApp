namespace Dottor.BrewerApp.Web.Pages
{
    using Dottor.BrewerApp.Common.Models;
    using Dottor.BrewerApp.Common.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class IndexModel : PageModel
    {
        private readonly IBrewerService _brewerService;
        private readonly ILogger<IndexModel> _logger;

        public IEnumerable<Beer> Beers { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, IBrewerService brewerService)
        {
            _logger = logger;
            _brewerService = brewerService;
        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                Beers = await _brewerService.GetBeersAsync();
                if (Beers is null)
                {
                    Response.StatusCode = 404;
                }
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on retrieve a list of beers.");
                return RedirectToPage("Error");
            }
        }
    }
}