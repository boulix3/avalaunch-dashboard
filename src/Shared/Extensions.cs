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
    public static string ToLocalDateTimeString(this long item)
    {
        return item.ToDateTimeOffset().LocalDateTime.ToString("G");
    }
    public static long[] ToLongArray(this IEnumerable<BigInteger> items)
    {
        return items.Select(x => x.ToLong()).ToArray();
    }
    public static long ToLong(this BigInteger item)
    {
        return (long)item;
    }

    public static bool IsValidAddress(this string address)
    {
        var result = Nethereum.Util.AddressUtil.Current.IsValidEthereumAddressHexFormat(address);
        Console.WriteLine("IsValidAddress + " + address + " - " + result);
        return result;
    }

    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> items) where T : struct
    {
        IEnumerable<T> nonNulls = items
                .Where(x => x.HasValue)
                .Select(x => x!.Value);
        return nonNulls;
    }

    public static string ToShortAddress(this string address)
    {
        if (address.Length > 8)
        {
            address = address.Substring(0, 5) + ".." + address.Substring(address.Length - 3);
        }
        return address;
    }
}