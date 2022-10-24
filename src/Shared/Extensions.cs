using System.Numerics;

namespace AvalaunchDashboard.Shared;

public static class Extensions
{
    public static string ToJson(this object item)
    {
        return System.Text.Json.JsonSerializer.Serialize(item);
    }

    public static DateTimeOffset ToDateTimeOffset(this long item)
    {
        return DateTimeOffset.FromUnixTimeSeconds(item);
    }
    public static string ToShortDateString(this long item)
    {
        return item.ToDateTimeOffset().Date.ToShortDateString();
    }
    public static string ToLongDateString(this long item)
    {
        return item.ToDateTimeOffset().Date.ToLongDateString();
    }
    public static long[] ToLongArray(this IEnumerable<BigInteger> items)
    {
        return items.Select(x => x.ToLong()).ToArray();
    }
    public static long ToLong(this BigInteger item)
    {
        return (long)item;
    }
}