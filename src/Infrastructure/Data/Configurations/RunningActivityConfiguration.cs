using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunningTracker.Domain;

namespace RunningTracker.Infrastructure;

public class RunningActivityConfiguration : IEntityTypeConfiguration<RunningActivity>
{
    public void Configure(EntityTypeBuilder<RunningActivity> builder)
    {
        builder.HasKey(runningActivity => runningActivity.Id);

        builder.HasOne(runningActivity => runningActivity.UserProfile)
               .WithMany(userProfile => userProfile.RunningActivities)
               .HasForeignKey(runningActivity => runningActivity.UserProfileId);
    }
}
