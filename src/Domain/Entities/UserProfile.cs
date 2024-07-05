namespace RunningTracker.Domain;

public class UserProfile
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Weight { get; set; } // kg
    public double Height { get; set; } // cm
    public DateTime BirthDate { get; set; }
    public int Age => DateTime.Now.Year - BirthDate.Year;
    public double BMI => Weight / Math.Pow(Height / 100, 2);

    public ICollection<RunningActivity>? RunningActivities { get; set; }
}
