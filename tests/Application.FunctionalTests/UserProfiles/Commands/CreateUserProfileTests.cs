using RunningTracker.Application.Common.Exceptions;
using RunningTracker.Application.Common.Extensions;
using RunningTracker.Application.UserProfiles.Commands.CreateUserProfile;
using RunningTracker.Domain;

namespace RunningTracker.Application.FunctionalTests;

using static Testing;
public class CreateUserProfileTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateUserProfileCommand();
        
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireUniqueName()
    {

        await SendAsync(new CreateUserProfileCommand
        {
            Name = "John Doe"
        });

        var command = new CreateUserProfileCommand
        {
            Name = "John Doe"
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateUserProfile()
    {

        var command = new CreateUserProfileCommand
        {
            Name = "Test First User",
            Weight = 87.6,
            Height = 179,
            BirthDate = "1996-07-06 14:31:00".ParseToUtc()

        };

        var result = await SendAsync(command);

        var list = await FindAsync<UserProfile>(result.Id);

        list.Should().NotBeNull();
        list!.Name.Should().Be(command.Name);
        list.Weight.Should().Be(command.Weight);
        list.Height.Should().Be(command.Height);
        list.BirthDate.Should().Be(command.BirthDate);
    }
}