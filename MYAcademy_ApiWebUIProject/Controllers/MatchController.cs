
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using SerieA.WebUI.ViewModels;

namespace SerieA.WebUI.Controllers
{
    public class MatchController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MatchController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IHttpClientFactory HttpClientFactory => _httpClientFactory;

        public async Task<IActionResult> Detail(int id)
        {
            var client = HttpClientFactory.CreateClient("SerieAApi");

            var match = await client.GetFromJsonAsync<MatchDetailViewModel>(
                $"api/Matches/{id}"
            );

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }
    }
}
