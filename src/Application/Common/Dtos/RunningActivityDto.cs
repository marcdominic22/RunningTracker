using RunningTracker.Domain;

namespace RunningTracker.Application.Common.Dtos;

public class RunningActivityDto
{
    public int Id { get; set; }
    public string? Location { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public double Distance { get; set; }

    public TimeSpan Duration { get; init; }

    public double AveragePace { get; init; }

    public UserProfileDto? UserProfile { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RunningActivity, RunningActivityDto>().ForMember(d => d.UserProfile, 
                opt => opt.MapFrom(s => s.UserProfile));
        }
    }
}
