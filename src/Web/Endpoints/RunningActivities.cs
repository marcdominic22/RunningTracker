using RunningTracker.Application;
using RunningTracker.Application.RunningActivities.Commands.CreateRunningActivity;
using RunningTracker.Application.RunningActivities.Queries.GetUserRunningActivities;

namespace RunningTracker.Web;

public class RunningActivities : EndpointGroupBase
{

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetUserRunningActivities,"GetUserRunningActivities")
            .MapPost(CreateRunningActivity);
    }

    public Task<UserRunningActivitiesVm> GetUserRunningActivities(ISender sender, int userId)
    {
        return sender.Send(new GetUserRunningActivitiesQuery(userId));
    }

    public Task<int> CreateRunningActivity(ISender sender, CreateRunningActivityCommand command)
    {
        return sender.Send(command);
    }

}
