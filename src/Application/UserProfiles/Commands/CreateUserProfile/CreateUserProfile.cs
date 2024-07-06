using RunningTracker.Application.Common.Dtos;
using RunningTracker.Application.Common.Interfaces;
using RunningTracker.Application.Common.Security;
using RunningTracker.Domain;

namespace RunningTracker.Application.UserProfiles.Commands.CreateUserProfile;

public record CreateUserProfileCommand : IRequest<UserProfileDto>
{
    public string? Name { get; init; }

    public double Weight { get; init; }

    public double Height { get; init; }

    public DateTime BirthDate { get; set; }
}

public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, UserProfileDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateUserProfileCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserProfileDto> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var entity = new UserProfile
        {
            Name = request.Name,
            BirthDate = request.BirthDate,
            Weight = request.Weight,
            Height = request.Height
        };

        _context.UserProfiles.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserProfileDto>(entity);
    }
}
