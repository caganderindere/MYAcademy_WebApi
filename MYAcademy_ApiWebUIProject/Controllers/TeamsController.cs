
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace SerieA.WebUI.Controllers
{
    public class TeamsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TeamsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
                return BadRequest();

            var client = _httpClientFactory.CreateClient("SerieAApi");

            try
            {
                var team = await client.GetFromJsonAsync<TeamDetailViewModel>(
                    $"api/Teams/{id}");

                if (team == null)
                    return NotFound();

                var matches = await client.GetFromJsonAsync<List<TeamMatchViewModel>>(
                    "api/Matches");

                team.Matches = (matches ?? new List<TeamMatchViewModel>())
                    .Where(m => m.HomeTeamId == id || m.AwayTeamId == id)
                    .OrderByDescending(m => m.MatchDate)
                    .ToList();

                return View(team);
            }
            catch (HttpRequestException)
            {
                return StatusCode(502, "Takım bilgileri API üzerinden alınamadı.");
            }
        }
    }

    public class TeamDetailViewModel
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

        public List<TeamMatchViewModel> Matches { get; set; } = new();
    }

    public class TeamMatchViewModel
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
        public TeamInfoViewModel? HomeTeam { get; set; }
        public TeamInfoViewModel? AwayTeam { get; set; }
    }

    public class TeamInfoViewModel
    {
        public string Name { get; set; } = "";
    }
}
