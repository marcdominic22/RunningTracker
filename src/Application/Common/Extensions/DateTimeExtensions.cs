using System.Globalization;

namespace RunningTracker.Application.Common.Extensions;

public static class DateTimeExtensions
{
    public static DateTime ParseToUtc(this string dateString, string format = "yyyy-MM-dd HH:mm:ss")
    {
        return DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind).ToUniversalTime();
    }
}
