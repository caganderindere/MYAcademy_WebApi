
namespace SerieA.WebUI.ViewModels
{
    public class MatchDetailViewModel
    {
        public int MatchId { get; set; }
        public int Week { get; set; }
        public string Status { get; set; } = "";

        public string HomeTeam { get; set; } = "";
        public string AwayTeam { get; set; } = "";

        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }

        public DateTime MatchDate { get; set; }
        public string Time { get; set; } = "";
        public string Stadium { get; set; } = "";

        public List<MatchGoalViewModel> Goals { get; set; } = new();
        public List<MatchCardViewModel> Cards { get; set; } = new();
        public List<MatchSubstitutionViewModel> Substitutions { get; set; } = new();
    }

    public class MatchGoalViewModel
    {
        public string PlayerName { get; set; } = "";
        public int Minute { get; set; }
        public string Team { get; set; } = "";
    }

    public class MatchCardViewModel
    {
        public string PlayerName { get; set; } = "";
        public int Minute { get; set; }
        public string CardType { get; set; } = "";
        public string Team { get; set; } = "";
    }

    public class MatchSubstitutionViewModel
    {
        public string PlayerOut { get; set; } = "";
        public string PlayerIn { get; set; } = "";
        public int Minute { get; set; }
        public string Team { get; set; } = "";
    }
}
