using RunningTracker.Application.Common.Behaviours;
using RunningTracker.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RunningTracker.Application.UserProfiles.Commands.CreateUserProfile;

namespace RunningTracker.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateUserProfileCommand>> _logger = null!;
    private Mock<IUser> _user = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateUserProfileCommand>>();
        _user = new Mock<IUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateUserProfileCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateUserProfileCommand { Name = "Test First", Weight = 87.5,  Height = 178, BirthDate = DateTime.Parse("1996-07-06 14:30:00") }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateUserProfileCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateUserProfileCommand { Name = "Test First", Weight = 87.5,  Height = 178, BirthDate = DateTime.Parse("1996-07-06 14:30:00") }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
