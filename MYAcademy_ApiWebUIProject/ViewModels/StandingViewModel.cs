
namespace SerieA.WebUI.ViewModels
{
    public class StandingViewModel
    {
        public int Position { get; set; }

        public int TeamId { get; set; }
        public string Team { get; set; } = "";

        public int O { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public int M { get; set; }

        public int AG { get; set; }
        public int YG { get; set; }
        public int AV { get; set; }
        public int Puan { get; set; }
    }
}
