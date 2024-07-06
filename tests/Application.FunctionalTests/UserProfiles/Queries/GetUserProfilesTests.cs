namespace RunningTracker.Application.FunctionalTests;

using System.Globalization;
using Namotion.Reflection;
using RunningTracker.Application.Common.Extensions;
using RunningTracker.Application.UserProfiles.Queries.GetUserProfiles;
using RunningTracker.Domain;
using static Testing;

public class GetUserProfilesTests : BaseTestFixture
{
    
    [Test]
    public async Task ShouldReturnRunningActivity()
    {
        await AddListAsync(new List<UserProfile>
        {
            new() {
                Id = 3,
                Name = "Test First",
                Weight = 87.5,
                Height = 178,
                BirthDate = "1996-07-06 14:30:00".ParseToUtc(),
                RunningActivities = new List<RunningActivity>
                {
                    new() {
                        Id = 11,
                        Location = "Philippines",
                        StartDateTime = "2024-03-11 14:30:00".ParseToUtc(),
                        EndDateTime = "2024-03-11 17:30:00".ParseToUtc(),
                        Distance = 6.5
                    },
                    new() {
                        Id = 12,
                        Location = "Philippines",
                        StartDateTime = "2024-03-09 14:30:00".ParseToUtc(),
                        EndDateTime = "2024-03-09 15:30:00".ParseToUtc(),
                        Distance = 1.5
                    }
                }
            },
            new() {
                Id = 4,
                Name = "Test Second",
                Weight = 67.5,
                Height = 175,
                BirthDate = "1999-03-11 14:30:00".ParseToUtc(),
                RunningActivities =
                [
                    new RunningActivity
                    {
                        Id = 13,
                        Location = "Australia",
                        StartDateTime = "2024-03-11 14:30:00".ParseToUtc(),
                        EndDateTime = "2024-03-11 18:00:00".ParseToUtc(),
                        Distance = 6.5
                    },
                    new RunningActivity
                    {
                        Id = 14,
                        Location = "Australia",
                        StartDateTime = "2024-03-09 14:30:00".ParseToUtc(),
                        EndDateTime = "2024-03-09 15:30:00".ParseToUtc(),
                        Distance = 1.8
                    }
                ]
            },
        });

        var query = new GetUserProfilesQuery(true);

        var result = await SendAsync(query);

        result.Count().Should().BeGreaterThan(1);
        result.ForEach(x => x.RunningActivities.Should().NotBeEmpty());
        result.ForEach(x => x.RunningActivities.Count().Should().BeGreaterThan(1));
    }

    [Test]
    public async Task ShouldReturnAllListsOfUserProfile()
    {
        await AddListAsync(new List<UserProfile>
        {
            new() {
                Id = 11,
                Name = "Test First",
                Weight = 87.5,
                Height = 178,
                BirthDate = "1996-07-06 14:30:00".ParseToUtc(),
            },
            new() {
                Id = 22,
                Name = "Test Second",
                Weight = 67.5,
                Height = 175,
                BirthDate = "1999-03-11 14:30:00".ParseToUtc()
            },
            new() {
                Id = 33,
                Name = "Test Third",
                Weight = 90.5,
                Height = 188,
                BirthDate = "2001-02-11 14:30:00".ParseToUtc()
            },
        });

        var query = new GetUserProfilesQuery();

        var result = await SendAsync(query);

        result.Should().HaveCountGreaterThan(1);
        result.First(x => x.Id == 33).RunningActivities.Should().BeNullOrEmpty();
    }
}
