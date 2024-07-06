namespace RunningTracker.Application.FunctionalTests;

using RunningTracker.Application.Common.Exceptions;
using RunningTracker.Application.Common.Extensions;
using RunningTracker.Application.RunningActivities.Commands.CreateRunningActivity;
using RunningTracker.Domain;
using static Testing;
public class CreateRunningActivityTest : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateRunningActivityCommand();
        
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateRunningActivity()
    {
        var userId = await RunAsDefaultUserAsync();

        await AddListAsync(new List<UserProfile>
        {
            new() {
                Id = 1,
                Name = "Test First Run Activity",
                Weight = 87.5,
                Height = 178,
                BirthDate = "1996-07-06 14:30:00".ParseToUtc(),
            }
        });

        var command = new CreateRunningActivityCommand
        {
            UserProfileId = 1, 
            Location = "Philippines",
            StartDateTime =  "2024-07-06 14:30:00".ParseToUtc(), 
            EndDateTime = "2024-07-06 15:30:00".ParseToUtc(), 
            Distance = 3.2
        };

        var result = await SendAsync(command);

        var list = await FindAsync<RunningActivity>(result);

        list.Should().NotBeNull();
        command.UserProfileId.Should().BeOfType(typeof(int));
        result.Should().Be(list?.Id);
    }
}
