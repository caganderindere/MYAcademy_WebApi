
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_ApiProject.Context;
using MYAcademy_ApiProject.Entities;

namespace MYAcademy_ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ApiContext _context;

        public TeamsController(ApiContext context)
        {
            _context = context;
        }

        // TÜM TAKIMLARI GETİR
        [HttpGet]
        public IActionResult GetTeams()
        {
            var teams = _context.Teams
                .OrderBy(x => x.Name)
                .ToList();

            return Ok(teams);
        }

        // ID'YE GÖRE TAKIM GETİR
        [HttpGet("{id}")]
        public IActionResult GetTeam(int id)
        {
            var team = _context.Teams.Find(id);

            if (team == null)
                return NotFound("Takım bulunamadı.");

            return Ok(team);
        }

        // TAKIM EKLE
        [HttpPost]
        public IActionResult CreateTeam(Team team)
        {
            if (team == null)
                return BadRequest("Takım bilgileri boş olamaz.");

            if (string.IsNullOrWhiteSpace(team.Name) ||
                string.IsNullOrWhiteSpace(team.ShortName) ||
                string.IsNullOrWhiteSpace(team.City) ||
                string.IsNullOrWhiteSpace(team.Stadium))
            {
                return BadRequest("Zorunlu takım bilgilerini doldurun.");
            }

            if (team.Capacity < 0)
                return BadRequest("Stadyum kapasitesi negatif olamaz.");

            if (team.FoundationYear < 1800 ||
                team.FoundationYear > DateTime.Now.Year)
            {
                return BadRequest("Kuruluş yılı geçersiz.");
            }

            team.TeamId = 0;
            team.LogoUrl ??= "";

            _context.Teams.Add(team);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetTeam),
                new { id = team.TeamId },
                team);
        }

        // TAKIM GÜNCELLE
        [HttpPut]
        public IActionResult UpdateTeam(Team team)
        {
            if (team == null)
                return BadRequest("Takım bilgileri boş olamaz.");

            var existingTeam = _context.Teams
                .FirstOrDefault(x => x.TeamId == team.TeamId);

            if (existingTeam == null)
                return NotFound("Takım bulunamadı.");

            if (string.IsNullOrWhiteSpace(team.Name) ||
                string.IsNullOrWhiteSpace(team.ShortName) ||
                string.IsNullOrWhiteSpace(team.City) ||
                string.IsNullOrWhiteSpace(team.Stadium))
            {
                return BadRequest("Zorunlu takım bilgilerini doldurun.");
            }

            if (team.Capacity < 0)
                return BadRequest("Stadyum kapasitesi negatif olamaz.");

            if (team.FoundationYear < 1800 ||
                team.FoundationYear > DateTime.Now.Year)
            {
                return BadRequest("Kuruluş yılı geçersiz.");
            }

            existingTeam.Name = team.Name;
            existingTeam.ShortName = team.ShortName;
            existingTeam.City = team.City;
            existingTeam.Stadium = team.Stadium;
            existingTeam.Capacity = team.Capacity;
            existingTeam.FoundationYear = team.FoundationYear;
            existingTeam.LogoUrl = team.LogoUrl ?? "";
            existingTeam.Status = team.Status;

            _context.SaveChanges();

            return Ok(existingTeam);
        }

        // TAKIM SİL
        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            var team = _context.Teams.Find(id);

            if (team == null)
                return NotFound("Takım bulunamadı.");

            bool hasMatches = _context.Matches.Any(x =>
                x.HomeTeamId == id || x.AwayTeamId == id);

            if (hasMatches)
            {
                return BadRequest(
                    "Bu takımın maç kayıtları var. Önce ilişkili maçları yönetin.");
            }

            _context.Teams.Remove(team);
            _context.SaveChanges();

            return Ok("Takım başarıyla silindi.");
        }
    }
}
