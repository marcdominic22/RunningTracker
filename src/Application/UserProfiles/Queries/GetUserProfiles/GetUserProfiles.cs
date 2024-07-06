using RunningTracker.Application.Common.Dtos;
using RunningTracker.Application.Common.Extensions;
using RunningTracker.Application.Common.Interfaces;
using RunningTracker.Application.Common.Security;

namespace RunningTracker.Application.UserProfiles.Queries.GetUserProfiles;

public record GetUserProfilesQuery(bool includeRunningActivity = false) : IRequest<List<UserProfileDto>>;

public class GetUserProfilesQueryHandler : IRequestHandler<GetUserProfilesQuery, List<UserProfileDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserProfilesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UserProfileDto>> Handle(GetUserProfilesQuery request, CancellationToken cancellationToken)
    { 
        var userProfilesQuery = _context.UserProfiles.AsNoTracking();

        userProfilesQuery = userProfilesQuery.IfInclude(request.includeRunningActivity, query => query.Include(up => up.RunningActivities));

        var userProfileDtos = await userProfilesQuery
            .ProjectTo<UserProfileDto>(_mapper.ConfigurationProvider)
            .OrderBy(up => up.Id)
            .ToListAsync(cancellationToken);

        return userProfileDtos;
    }
}
