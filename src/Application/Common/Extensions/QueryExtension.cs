using Microsoft.EntityFrameworkCore.Query;

namespace RunningTracker.Application.Common.Extensions;

public static class QueryExtension
{
    public static IQueryable<T> IfInclude<T, TProperty>(
    this IQueryable<T> source, 
    bool condition, 
    Func<IQueryable<T>, IIncludableQueryable<T, TProperty>> include)
    where T : class
    {
        return condition ? include(source) : source;
    }
}