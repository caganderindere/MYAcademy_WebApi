using Microsoft.AspNetCore.Mvc;
using SerieA.WebUI.ViewModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace MYAcademy_ApiWebUIProject.Controllers
{
    public class FixturesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FixturesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int week = 1)
        {
            if (week < 1) week = 1;
            if (week > 38) week = 38;

            var client = _httpClientFactory.CreateClient("SerieAApi");
            var vm = new FixturesPageViewModel { Week = week };

            // 1) Haftanın maçları (gol / kart / değişiklik olaylarıyla birlikte)
            try
            {
                var response = await client.GetAsync($"api/Matches/Week/{week}");

                if (response.IsSuccessStatusCode)
                {
                    var matches = await response.Content.ReadFromJsonAsync<List<MatchDto>>()
                                  ?? new List<MatchDto>();

                    vm.Matches = matches
                        .OrderBy(x => x.MatchDate)
                        .ThenBy(x => x.Time)
                        .ToList();
                }
                else
                {
                    ViewBag.Error = $"API hata döndürdü (HTTP {(int)response.StatusCode}).";
                }
            }
            catch (HttpRequestException)
            {
                ViewBag.Error = "API'ye ulaşılamıyor. API projesinin (https://localhost:7009) çalıştığından emin ol.";
                return View("Index", vm);
            }
            catch (JsonException ex)
            {
                ViewBag.Error = "API yanıtı okunamadı: " + ex.Message;
            }

            // 2) Puan durumu
            try
            {
                var standings = await client.GetFromJsonAsync<List<StandingViewModel>>("api/Standings")
                                ?? new List<StandingViewModel>();

                for (int i = 0; i < standings.Count; i++)
                    standings[i].Position = i + 1;

                vm.Standings = standings;
            }
            catch (Exception) { /* puan durumu yoksa sayfa yine açılır */ }

            // 3) Takımlar (logo şeridi için)
            try
            {
                vm.Teams = await client.GetFromJsonAsync<List<TeamDto>>("api/Teams")
                           ?? new List<TeamDto>();
            }
            catch (Exception) { /* takım listesi yoksa sayfa yine açılır */ }

            return View("Index", vm);
        }
    }

    public class FixturesPageViewModel
    {
        public int Week { get; set; }

        public List<MatchDto> Matches { get; set; } = new();
        public List<StandingViewModel> Standings { get; set; } = new();
        public List<TeamDto> Teams { get; set; } = new();

        public int TotalMatches => Matches.Count;

        public int CompletedMatches =>
            Matches.Count(m => m.HomeScore.HasValue && m.AwayScore.HasValue);

        public int TotalGoals =>
            Matches.Where(m => m.HomeScore.HasValue && m.AwayScore.HasValue)
                   .Sum(m => m.HomeScore!.Value + m.AwayScore!.Value);

        public int HomeWins =>
            Matches.Count(m => m.HomeScore.HasValue && m.AwayScore.HasValue && m.HomeScore > m.AwayScore);

        public int Draws =>
            Matches.Count(m => m.HomeScore.HasValue && m.AwayScore.HasValue && m.HomeScore == m.AwayScore);

        public int AwayWins =>
            Matches.Count(m => m.HomeScore.HasValue && m.AwayScore.HasValue && m.HomeScore < m.AwayScore);
    }

    public class MatchDto
    {
        public int MatchId { get; set; }
        public int Week { get; set; }
        public string Status { get; set; } = "";
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public TeamDto HomeTeam { get; set; } = new();
        public TeamDto AwayTeam { get; set; } = new();
        public DateTime MatchDate { get; set; }
        public string Time { get; set; } = "";
        public string Stadium { get; set; } = "";
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }

        public List<GoalEventDto> Goals { get; set; } = new();
        public List<CardEventDto> Cards { get; set; } = new();
        public List<SubEventDto> Substitutions { get; set; } = new();
    }

    public class TeamDto
    {
        public int TeamId { get; set; }
        public string Name { get; set; } = "";
        public string ShortName { get; set; } = "";
        public string LogoUrl { get; set; } = "";
    }

    public class GoalEventDto
    {
        public string PlayerName { get; set; } = "";
        public int Minute { get; set; }
        public string Team { get; set; } = "";
    }

    public class CardEventDto
    {
        public string PlayerName { get; set; } = "";
        public int Minute { get; set; }
        public string CardType { get; set; } = "";
        public string Team { get; set; } = "";
    }

    public class SubEventDto
    {
        public string PlayerOut { get; set; } = "";
        public string PlayerIn { get; set; } = "";
        public int Minute { get; set; }
        public string Team { get; set; } = "";
    }
}