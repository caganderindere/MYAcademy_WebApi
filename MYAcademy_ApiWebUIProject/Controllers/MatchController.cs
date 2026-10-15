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

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (id <= 0)
                return NotFound();

            var client = _httpClientFactory.CreateClient("SerieAApi");

            try
            {
                var match = await client.GetFromJsonAsync<MatchDetailViewModel>(
                    $"api/Matches/{id}"
                );

                if (match == null)
                    return NotFound();

                return View("Detail", match);
            }
            catch (HttpRequestException)
            {
                return StatusCode(
                    502,
                    "Maç bilgilerine ulaşılırken API bağlantı hatası oluştu."
                );
            }
            catch (System.Text.Json.JsonException)
            {
                return StatusCode(
                    502,
                    "API'den gelen maç verileri okunamadı."
                );
            }
        }
    }
}