using RunningTracker.Domain;

namespace RunningTracker.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<UserProfile> UserProfiles { get; }

    DbSet<RunningActivity> RunningActivities { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
