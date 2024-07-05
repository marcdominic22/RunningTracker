using RunningTracker.Application.Common.Dtos;
using RunningTracker.Application.Common.Interfaces;
using RunningTracker.Application.Common.Security;

namespace RunningTracker.Application.RunningActivities.Queries.GetUserRunningActivities;

public record GetUserRunningActivitiesQuery(int id) : IRequest<UserRunningActivitiesVm>;

public class GetUserRunningActivitiesQueryHandler : IRequestHandler<GetUserRunningActivitiesQuery, UserRunningActivitiesVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserRunningActivitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserRunningActivitiesVm> Handle(GetUserRunningActivitiesQuery request, CancellationToken cancellationToken)
    { 
        var query = await _context.UserProfiles.Where(u => u.Id == request.id)
                                               .Include(u => u.RunningActivities)
                                               .FirstOrDefaultAsync(cancellationToken);

        if (query == null)
        {
            throw new Exception("User profile not found");
        }

        var runningActivityDtos = query.RunningActivities?
            .Select(activity => new RunningActivityDto
            {
                Id = activity.Id,
                Location = activity.Location,
                StartDateTime = activity.StartDateTime,
                EndDateTime = activity.EndDateTime,
                Distance = activity.Distance,
                Duration = activity.Duration,
                AveragePace = activity.AveragePace
            }).ToList() ?? new List<RunningActivityDto>();


        return new UserRunningActivitiesVm
            {
                Name = query.Name,
                UserProfileId = query.Id,

                Lists = runningActivityDtos.AsReadOnly() 
            };
    }
}
