using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Security.Pages
{
    public class AccessDenied : PageModel
    {
        private readonly ILogger<AccessDenied> _logger;

        public AccessDenied(ILogger<AccessDenied> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}