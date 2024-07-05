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
        return await _context.UserProfiles
                .AsNoTracking()
                .If(request.includeRunningActivity, userProfile => userProfile.Include(u => u.RunningActivities))
                .ProjectTo<UserProfileDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Id)
                .ToListAsync(cancellationToken);
    }
}
