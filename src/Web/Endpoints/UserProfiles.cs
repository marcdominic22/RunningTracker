using RunningTracker.Application.Common.Dtos;
using RunningTracker.Application.UserProfiles.Commands.CreateUserProfile;
using RunningTracker.Application.UserProfiles.Queries.GetUserProfiles;

namespace RunningTracker.Web;

public class UserProfiles : EndpointGroupBase
{

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetUserProfiles, "GetUserProfileList")
            .MapPost(CreateUserProfile);
    }

    public Task<List<UserProfileDto>> GetUserProfiles(ISender sender, bool includeRunningActivity)
    {
        return  sender.Send(new GetUserProfilesQuery(includeRunningActivity));
    }

    public Task<UserProfileDto> CreateUserProfile(ISender sender, CreateUserProfileCommand command)
    {
        return sender.Send(command);
    }

}
