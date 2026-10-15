
namespace MYAcademy_ApiProject.Entities
{
    public class Match
    {
        public int MatchId { get; set; }

        public int Week { get; set; }

        public string Status { get; set; } = "Başlamadı";

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public DateTime MatchDate { get; set; }

        public string Time { get; set; } = "";

        public string Stadium { get; set; } = "";

        public int? HomeScore { get; set; }

        public int? AwayScore { get; set; }

        public Team? HomeTeam { get; set; }

        public Team? AwayTeam { get; set; }

        public List<MatchGoal> MatchGoals { get; set; } = new();

        public List<MatchCard> MatchCards { get; set; } = new();

        public List<MatchSubstitution> MatchSubstitutions { get; set; } = new();
    }
}
