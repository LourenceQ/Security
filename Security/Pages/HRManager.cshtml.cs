using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Security.Authorization;
using Security.DTO;
using Security.Pages.Account;

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

            var res = await httpClient
                .PostAsJsonAsync("auth", new Credential { UserName = "admin", Password = "123456" });
            
            res.EnsureSuccessStatusCode();

            string  strJwt = await res.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<JwtToken>(strJwt);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", token.AccessToken);

            WeatherForecastItems = await httpClient
                .GetFromJsonAsync<List<WeatherForecastDto>>("WeatherForecast");
        }
    }
}