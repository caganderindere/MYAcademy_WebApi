
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using SerieA.WebUI.ViewModels;

namespace SerieA.WebUI.Controllers
{
    public class StandingsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StandingsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("SerieAApi");

            var standings = await client.GetFromJsonAsync<List<StandingViewModel>>(
                "api/Standings"
            );

            if (standings == null)
            {
                return View(new List<StandingViewModel>());
            }

            for (int i = 0; i < standings.Count; i++)
            {
                standings[i].Position = i + 1;
            }

            return View(standings);
        }
    }
}
