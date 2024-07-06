using RunningTracker.Application.Common.Interfaces;
using RunningTracker.Application.Common.Security;
using RunningTracker.Domain;

namespace RunningTracker.Application.RunningActivities.Commands.CreateRunningActivity;

public record CreateRunningActivityCommand : IRequest<int>
{
    public int UserProfileId { get; init; }
    public string? Location { get; init; }
    public DateTime StartDateTime { get; init; }
    public DateTime EndDateTime { get; set; }
    public double Distance { get; set; }
}

public class CreateRunningActivityCommandHandler : IRequestHandler<CreateRunningActivityCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateRunningActivityCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateRunningActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = new RunningActivity
        {
            UserProfileId = request.UserProfileId,
            Location = request.Location,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            Distance = request.Distance
        };

        _context.RunningActivities.Add(activity);
        await _context.SaveChangesAsync(cancellationToken);

        return activity.Id;
    }
}
