namespace SteamGameViewerApp.Models
{
    public class SteamGame
    {
        public string Name { get; set; }
        public int AppId { get; set; }
        public double Hours { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Hours:F2} hrs";
        }

        public override bool Equals(object obj)
        {
            return obj is SteamGame other && this.AppId == other.AppId;
        }

        public override int GetHashCode()
        {
            return AppId.GetHashCode();
        }
    }
}
