namespace RunningTracker.Application.Common.Extensions;

public static class QueryExtension
{
    public static IQueryable<T> If<T>(this IQueryable<T> source, bool condition, Func<IQueryable<T>, IQueryable<T>> transform)
    {
        return condition ? transform(source) : source;
    }
}