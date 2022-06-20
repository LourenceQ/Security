using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Security.Pages
{
    [Authorize(Policy="AdminOnly")]
    public class Settings : PageModel
    {
        private readonly ILogger<Settings> _logger;

        public Settings(ILogger<Settings> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}