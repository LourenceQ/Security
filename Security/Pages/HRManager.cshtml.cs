using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Security.DTO;

namespace Security.Pages
{
    [Authorize(Policy="HRManagerOnly")]
    public class HRManager : PageModel
    {
        [BindProperty]
        public List<WeatherForecastDto> WeatherForecastItems { get; set; }
        private readonly ILogger<HRManager> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HRManager(ILogger<HRManager> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("API");
            WeatherForecastItems = await httpClient.GetFromJsonAsync<List<WeatherForecastDto>>("WeatherForecast");
        }
    }
}