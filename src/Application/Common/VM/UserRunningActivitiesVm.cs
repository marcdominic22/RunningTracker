using RunningTracker.Application.Common.Dtos;

namespace RunningTracker.Application;

public class UserRunningActivitiesVm
{
    public int UserProfileId { get; init; }
    public string? Name { get; init; }
    public IReadOnlyCollection<RunningActivityDto> Lists { get; init; } = Array.Empty<RunningActivityDto>();
}
