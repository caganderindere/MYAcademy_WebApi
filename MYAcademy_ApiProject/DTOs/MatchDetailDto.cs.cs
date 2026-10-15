namespace MYAcademy_ApiProject.DTOs
{
    public class MatchDetailDto
    {
        public int MatchId { get; set; }
        public int Week { get; set; }
        public string Status { get; set; }

        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

        // Takım logoları
        public string HomeTeamLogoUrl { get; set; }
        public string AwayTeamLogoUrl { get; set; }

        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }

        public DateTime MatchDate { get; set; }
        public string Time { get; set; }
        public string Stadium { get; set; }

        public List<MatchGoalDto> Goals { get; set; }
        public List<MatchCardDto> Cards { get; set; }
        public List<MatchSubstitutionDto> Substitutions { get; set; }
    }
}