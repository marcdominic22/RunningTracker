namespace RunningTracker.Domain;

public class RunningActivity
{
    public int Id { get; set; }
    public string? Location { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public double Distance { get; set; } // km
    public TimeSpan Duration => EndDateTime - StartDateTime;
    public double AveragePace => Duration.TotalMinutes / Distance;

    public int UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
}
