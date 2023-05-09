namespace WorkoutTracker.WebAPI.Models
{
    public class ExternalLoginModel
    {
        public string? Provider { get; set; }
        public string? IdToken { get; set; }
    }
}
