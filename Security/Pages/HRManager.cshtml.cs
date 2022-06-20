using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Security.Pages
{
    [Authorize(Policy="HRManagerOnly")]
    public class HRManager : PageModel
    {
        private readonly ILogger<HRManager> _logger;

        public HRManager(ILogger<HRManager> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}