using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_ApiProject.Context;
using MYAcademy_ApiProject.Entities;
using MYAcademy_ApiProject.DTOs;

namespace MYAcademy_ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly ApiContext _context;

        public MatchesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Matches
        [HttpGet]
        public IActionResult GetMatches()
        {
            var values = _context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .ToList();

            return Ok(values);
        }

        // GET: api/Matches/5
        [HttpGet("{id}")]
        public IActionResult GetMatch(int id)
        {
            var value = _context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.MatchGoals)
                    .ThenInclude(x => x.Team)
                .Include(x => x.MatchCards)
                    .ThenInclude(x => x.Team)
                .Include(x => x.MatchSubstitutions)
                    .ThenInclude(x => x.Team)
                .FirstOrDefault(x => x.MatchId == id);

            if (value == null)
            {
                return NotFound("Maç bulunamadı.");
            }

            var result = new MatchDetailDto
            {
                MatchId = value.MatchId,
                Week = value.Week,
                Status = value.Status,

                HomeTeam = value.HomeTeam.Name,
                AwayTeam = value.AwayTeam.Name,

                HomeTeamLogoUrl = value.HomeTeam.LogoUrl,
                AwayTeamLogoUrl = value.AwayTeam.LogoUrl,

                HomeScore = value.HomeScore,
                AwayScore = value.AwayScore,

                MatchDate = value.MatchDate,
                Time = value.Time,
                Stadium = value.Stadium,

                Goals = value.MatchGoals.Select(x => new MatchGoalDto
                {
                    PlayerName = x.PlayerName,
                    Minute = x.Minute,
                    Team = x.Team.Name
                }).ToList(),

                Cards = value.MatchCards.Select(x => new MatchCardDto
                {
                    PlayerName = x.PlayerName,
                    Minute = x.Minute,
                    CardType = x.CardType,
                    Team = x.Team.Name
                }).ToList(),

                Substitutions = value.MatchSubstitutions.Select(x => new MatchSubstitutionDto
                {
                    PlayerOut = x.PlayerOut,
                    PlayerIn = x.PlayerIn,
                    Minute = x.Minute,
                    Team = x.Team.Name
                }).ToList()
            };

            return Ok(result);
        }

        // GET: api/Matches/Week/1
        // Fikstür sayfası için: maçlar + gol / kart / oyuncu değişikliği olayları.
        // Entity'ler doğrudan döndürülmüyor (Match <-> MatchGoal döngüsü JSON hatası verir),
        // bu yüzden DTO'ya projeksiyon yapılıyor.
        [HttpGet("Week/{week}")]
        public IActionResult GetMatchesByWeek(int week)
        {
            var matches = _context.Matches
                .Where(x => x.Week == week)
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.MatchGoals).ThenInclude(g => g.Team)
                .Include(x => x.MatchCards).ThenInclude(c => c.Team)
                .Include(x => x.MatchSubstitutions).ThenInclude(s => s.Team)
                .AsSplitQuery()
                .ToList();

            var result = matches
                .OrderBy(x => x.MatchDate)
                .ThenBy(x => x.Time)
                .Select(x => new
                {
                    x.MatchId,
                    x.Week,
                    x.Status,
                    x.HomeTeamId,
                    x.AwayTeamId,
                    x.MatchDate,
                    x.Time,
                    x.Stadium,
                    x.HomeScore,
                    x.AwayScore,

                    HomeTeam = new
                    {
                        x.HomeTeam!.TeamId,
                        x.HomeTeam.Name,
                        x.HomeTeam.ShortName,
                        x.HomeTeam.LogoUrl
                    },
                    AwayTeam = new
                    {
                        x.AwayTeam!.TeamId,
                        x.AwayTeam.Name,
                        x.AwayTeam.ShortName,
                        x.AwayTeam.LogoUrl
                    },

                    Goals = x.MatchGoals
                        .OrderBy(g => g.Minute)
                        .Select(g => new MatchGoalDto
                        {
                            PlayerName = g.PlayerName,
                            Minute = g.Minute,
                            Team = g.Team.Name
                        }).ToList(),

                    Cards = x.MatchCards
                        .OrderBy(c => c.Minute)
                        .Select(c => new MatchCardDto
                        {
                            PlayerName = c.PlayerName,
                            Minute = c.Minute,
                            CardType = c.CardType,
                            Team = c.Team.Name
                        }).ToList(),

                    Substitutions = x.MatchSubstitutions
                        .OrderBy(s => s.Minute)
                        .Select(s => new MatchSubstitutionDto
                        {
                            PlayerOut = s.PlayerOut,
                            PlayerIn = s.PlayerIn,
                            Minute = s.Minute,
                            Team = s.Team.Name
                        }).ToList()
                })
                .ToList();

            return Ok(result);
        }

        // POST: api/Matches
        [HttpPost]
        public IActionResult CreateMatch(Match match)
        {
            if (match.HomeTeamId == match.AwayTeamId)
            {
                return BadRequest("Ev sahibi ve deplasman takımı aynı olamaz.");
            }

            if (_context.Matches.Any(x =>
                x.Week == match.Week &&
                (
                    x.HomeTeamId == match.HomeTeamId ||
                    x.AwayTeamId == match.HomeTeamId ||
                    x.HomeTeamId == match.AwayTeamId ||
                    x.AwayTeamId == match.AwayTeamId
                )))
            {
                return BadRequest("Bu takımlardan biri aynı hafta içinde zaten maç yapıyor.");
            }

            if (match.Status == "Tamamlandı" &&
                (match.HomeScore == null || match.AwayScore == null))
            {
                return BadRequest("Tamamlanan maçlarda skor bilgisi zorunludur.");
            }

            _context.Matches.Add(match);
            _context.SaveChanges();

            return Ok(match);
        }

        // PUT: api/Matches
        [HttpPut]
        public IActionResult UpdateMatch(Match match)
        {
            _context.Matches.Update(match);
            _context.SaveChanges();

            return Ok(match);
        }

        // DELETE: api/Matches/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMatch(int id)
        {
            var value = _context.Matches.Find(id);

            if (value == null)
            {
                return NotFound("Maç bulunamadı.");
            }

            _context.Matches.Remove(value);
            _context.SaveChanges();

            return Ok(value);
        }
    }
}