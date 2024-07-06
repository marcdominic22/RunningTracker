using RunningTracker.Application.Common.Interfaces;
using RunningTracker.Application.RunningActivities.Commands.CreateRunningActivity;

namespace RunningTracker.Application;

public class CreateRunningActivityValidator : AbstractValidator<CreateRunningActivityCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateRunningActivityValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Location)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.UserProfileId)
            .NotNull();

        RuleFor(v => v.StartDateTime)
            .NotNull();

        RuleFor(v => v.EndDateTime)
            .NotNull();

        RuleFor(v => v.Distance)
            .NotNull();
    }

}