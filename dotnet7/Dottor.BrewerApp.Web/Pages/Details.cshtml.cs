using Dottor.BrewerApp.Common.Models;
using Dottor.BrewerApp.Common.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dottor.BrewerApp.Web.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IBrewerService _brewerService;
        private readonly ILogger<DetailsModel> _logger;

        public Beer? Beer { get; private set; }

        public DetailsModel(IBrewerService brewerService, ILogger<DetailsModel> logger)
        {
            _brewerService = brewerService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                Beer = await _brewerService.GetBeerAsync(id);
                if(Beer is null)
                {
                    Response.StatusCode = 404;
                }
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on retrieve a beer with id '{berrId}'.", id);
                return RedirectToPage("Error");
            }
        }
    }
}
