
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;

namespace MYAcademy_ApiWebUIProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public AdminController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient ApiClient()
        {
            return _httpClientFactory.CreateClient("SerieAApi");
        }

        // =========================
        // MAÇ YÖNETİMİ
        // =========================

        // GET: /Admin/Matches
        [HttpGet]
        public async Task<IActionResult> Matches()
        {
            var client = ApiClient();

            try
            {
                var matches = await client.GetFromJsonAsync<List<AdminMatchViewModel>>(
                    "api/Matches", _jsonOptions);

                var teams = await client.GetFromJsonAsync<List<AdminTeamViewModel>>(
                    "api/Teams", _jsonOptions);

                var model = new AdminMatchesPageViewModel
                {
                    Matches = matches ?? new(),
                    Teams = teams ?? new()
                };

                return View(model);
            }
            catch (HttpRequestException)
            {
                return StatusCode(502,
                    "API'ye bağlanılamadı. API projesinin çalıştığını kontrol edin.");
            }
            catch (JsonException)
            {
                return StatusCode(502, "API verileri okunamadı.");
            }
        }

        // POST: /Admin/CreateMatch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMatch(AdminMatchFormModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["AdminError"] = "Lütfen form alanlarını kontrol edin.";
                return RedirectToAction(nameof(Matches));
            }

            if (model.HomeTeamId == model.AwayTeamId)
            {
                TempData["AdminError"] =
                    "Ev sahibi ve deplasman takımı aynı olamaz.";

                return RedirectToAction(nameof(Matches));
            }

            var payload = new
            {
                week = model.Week,
                status = model.Status,
                homeTeamId = model.HomeTeamId,
                awayTeamId = model.AwayTeamId,
                matchDate = model.MatchDate,
                time = model.Time,
                stadium = model.Stadium ?? "",
                homeScore = model.HomeScore,
                awayScore = model.AwayScore
            };

            var response = await ApiClient().PostAsJsonAsync(
                "api/Matches", payload);

            if (response.IsSuccessStatusCode)
                TempData["AdminSuccess"] = "Maç başarıyla eklendi.";
            else
                TempData["AdminError"] =
                    await response.Content.ReadAsStringAsync();

            return RedirectToAction(nameof(Matches));
        }

        // POST: /Admin/UpdateMatch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMatch(AdminMatchFormModel model)
        {
            if (model.MatchId <= 0 || !ModelState.IsValid)
            {
                TempData["AdminError"] = "Maç bilgileri geçersiz.";
                return RedirectToAction(nameof(Matches));
            }

            if (model.HomeTeamId == model.AwayTeamId)
            {
                TempData["AdminError"] =
                    "Ev sahibi ve deplasman takımı aynı olamaz.";

                return RedirectToAction(nameof(Matches));
            }

            var payload = new
            {
                matchId = model.MatchId,
                week = model.Week,
                status = model.Status,
                homeTeamId = model.HomeTeamId,
                awayTeamId = model.AwayTeamId,
                matchDate = model.MatchDate,
                time = model.Time,
                stadium = model.Stadium ?? "",
                homeScore = model.HomeScore,
                awayScore = model.AwayScore
            };

            var response = await ApiClient().PutAsJsonAsync(
                "api/Matches", payload);

            if (response.IsSuccessStatusCode)
                TempData["AdminSuccess"] = "Maç başarıyla güncellendi.";
            else
                TempData["AdminError"] =
                    await response.Content.ReadAsStringAsync();

            return RedirectToAction(nameof(Matches));
        }

        // POST: /Admin/DeleteMatch/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            if (id <= 0)
            {
                TempData["AdminError"] = "Geçersiz maç numarası.";
                return RedirectToAction(nameof(Matches));
            }

            var response = await ApiClient().DeleteAsync(
                $"api/Matches/{id}");

            if (response.IsSuccessStatusCode)
                TempData["AdminSuccess"] = "Maç silindi.";
            else
                TempData["AdminError"] =
                    await response.Content.ReadAsStringAsync();

            return RedirectToAction(nameof(Matches));
        }

        // =========================
        // TAKIM YÖNETİMİ
        // =========================

        // GET: /Admin/Teams
        [HttpGet]
        public async Task<IActionResult> Teams()
        {
            try
            {
                var teams = await ApiClient()
                    .GetFromJsonAsync<List<AdminTeamViewModel>>(
                        "api/Teams", _jsonOptions);

                return View(teams ?? new List<AdminTeamViewModel>());
            }
            catch (HttpRequestException)
            {
                return StatusCode(502,
                    "API'ye bağlanılamadı. API projesinin çalıştığını kontrol edin.");
            }
            catch (JsonException)
            {
                return StatusCode(502, "Takım verileri okunamadı.");
            }
        }

        // POST: /Admin/CreateTeam
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeam(AdminTeamFormModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["AdminError"] = "Takım bilgilerini kontrol edin.";
                return RedirectToAction(nameof(Teams));
            }

            var payload = new
            {
                name = model.Name,
                shortName = model.ShortName,
                city = model.City,
                stadium = model.Stadium,
                capacity = model.Capacity,
                foundationYear = model.FoundationYear,
                logoUrl = model.LogoUrl ?? "",
                status = model.Status
            };

            var response = await ApiClient().PostAsJsonAsync(
                "api/Teams", payload);

            if (response.IsSuccessStatusCode)
                TempData["AdminSuccess"] = "Takım başarıyla eklendi.";
            else
                TempData["AdminError"] =
                    await response.Content.ReadAsStringAsync();

            return RedirectToAction(nameof(Teams));
        }

        // POST: /Admin/UpdateTeam
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTeam(AdminTeamFormModel model)
        {
            if (model.TeamId <= 0 || !ModelState.IsValid)
            {
                TempData["AdminError"] = "Takım bilgileri geçersiz.";
                return RedirectToAction(nameof(Teams));
            }

            var payload = new
            {
                teamId = model.TeamId,
                name = model.Name,
                shortName = model.ShortName,
                city = model.City,
                stadium = model.Stadium,
                capacity = model.Capacity,
                foundationYear = model.FoundationYear,
                logoUrl = model.LogoUrl ?? "",
                status = model.Status
            };

            var response = await ApiClient().PutAsJsonAsync(
                "api/Teams", payload);

            if (response.IsSuccessStatusCode)
                TempData["AdminSuccess"] = "Takım başarıyla güncellendi.";
            else
                TempData["AdminError"] =
                    await response.Content.ReadAsStringAsync();

            return RedirectToAction(nameof(Teams));
        }

        // POST: /Admin/DeleteTeam/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            if (id <= 0)
            {
                TempData["AdminError"] = "Geçersiz takım numarası.";
                return RedirectToAction(nameof(Teams));
            }

            var response = await ApiClient().DeleteAsync(
                $"api/Teams/{id}");

            if (response.IsSuccessStatusCode)
                TempData["AdminSuccess"] = "Takım silindi.";
            else
                TempData["AdminError"] =
                    await response.Content.ReadAsStringAsync();

            return RedirectToAction(nameof(Teams));
        }
    }

    // =========================
    // VIEWMODEL SINIFLARI
    // =========================

    public class AdminMatchesPageViewModel
    {
        public List<AdminMatchViewModel> Matches { get; set; } = new();
        public List<AdminTeamViewModel> Teams { get; set; } = new();
    }

    public class AdminMatchViewModel
    {
        public int MatchId { get; set; }
        public int Week { get; set; }
        public string Status { get; set; } = "";
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime MatchDate { get; set; }
        public string Time { get; set; } = "";
        public string Stadium { get; set; } = "";
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }

        public AdminTeamViewModel? HomeTeam { get; set; }
        public AdminTeamViewModel? AwayTeam { get; set; }
    }

    public class AdminTeamViewModel
    {
        public int TeamId { get; set; }
        public string Name { get; set; } = "";
        public string ShortName { get; set; } = "";
        public string City { get; set; } = "";
        public string Stadium { get; set; } = "";
        public int Capacity { get; set; }
        public int FoundationYear { get; set; }
        public string LogoUrl { get; set; } = "";
        public bool Status { get; set; }
    }

    public class AdminMatchFormModel
    {
        public int MatchId { get; set; }
        public int Week { get; set; }
        public string Status { get; set; } = "Başlamadı";
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime MatchDate { get; set; }
        public string Time { get; set; } = "";
        public string? Stadium { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
    }

    public class AdminTeamFormModel
    {
        public int TeamId { get; set; }
        public string Name { get; set; } = "";
        public string ShortName { get; set; } = "";
        public string City { get; set; } = "";
        public string Stadium { get; set; } = "";
        public int Capacity { get; set; }
        public int FoundationYear { get; set; }
        public string? LogoUrl { get; set; }
        public bool Status { get; set; } = true;
    }
}
