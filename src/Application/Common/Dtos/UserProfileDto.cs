using RunningTracker.Domain;

namespace RunningTracker.Application.Common.Dtos;

public class UserProfileDto
{
    public int Id { get; init; }

    public string? Name { get; init; }

    public double Weight { get; init; }

    public double Height { get; init; }

    public DateTime BirthDate { get; set; }

    public int Age { get; init; }

    public double BMI { get; init; }

    public List<RunningActivityDto> RunningActivities { get; init; } = new List<RunningActivityDto>();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserProfile, UserProfileDto>().ForMember(d => d.RunningActivities, 
                opt => opt.MapFrom(s => s.RunningActivities));
        }
    }
}
