using System.Linq;
using AvalaunchDashboard.Shared;

namespace AvalaunchDashboard.Client.Pages;
public class UnlockData
{
    public UnlockDataItem[] Items { get; }
    public UnlockData() : this(new()) { }
    public UnlockData(Dictionary<string, SaleInfo> saleInfos)
    {
        var data = new List<UnlockDataItem>();
        foreach (var item in saleInfos)
        {
            var totalCount = item.Value.VestingTimes.Length;
            for (int i = 0; i < totalCount; i++)
            {
                var date = item.Value.VestingTimes[i].ToDateTimeOffset().LocalDateTime;
                var percent = (decimal)item.Value.VestingPortions[i] / item.Value.VestingPortionPrecision;
                data.Add(new UnlockDataItem(date, item.Value.TokenAddress, item.Value.TokenSymbol, item.Value.Address, percent, i, totalCount));
            }
        }
        Items = data.OrderBy(x => x.Date).ToArray();
    }
}

public record UnlockDataItem(DateTime Date, string TokenAddress, string TokenSymbol, string SaleAddress, decimal Percent, int ItemIndex, int TotalCount);