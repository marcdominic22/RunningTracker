using RunningTracker.Application.Common.Interfaces;
using RunningTracker.Application.UserProfiles.Commands.CreateUserProfile;

namespace RunningTracker.Application.UserProfiles.Commands;

public class CreateUserProfileValidator : AbstractValidator<CreateUserProfileCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateUserProfileValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueName)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");

        RuleFor(v => v.Height)
            .NotNull();

        RuleFor(v => v.Weight)
            .NotNull();

        RuleFor(v => v.BirthDate)
            .NotNull();
    }

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _context.UserProfiles
            .AllAsync(l => l.Name != name, cancellationToken);
    }
}
