namespace Ramand_Application.Model
{
    public class JWTSettings
    {
        public string Key { get; set; } = string.Empty;
        public string IsSure { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }
    }
}