using RunningTracker.Domain;
using RunningTracker.Domain.Entities;

namespace RunningTracker.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<UserProfile> UserProfiles { get; }

    DbSet<RunningActivity> RunningActivities { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
