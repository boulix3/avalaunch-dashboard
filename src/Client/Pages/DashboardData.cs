using AvalaunchDashboard.Shared;

namespace AvalaunchDashboard.Client.Pages;
public class DashboardData
{
    public DashboardDataItem[] Data { get; set; }
    public DashboardData() : this(new Dictionary<string, SaleInfo>(), new Dictionary<string, UserVestingInfo>(), new Dictionary<string, decimal>())
    {
    }
    public DashboardData(Dictionary<string, SaleInfo> saleInfos, Dictionary<string, UserVestingInfo> userInfos, Dictionary<string, decimal> prices)
    {
        List<DashboardDataItem> data = new();
        foreach (var key in userInfos.Keys)
        {
            if (saleInfos.ContainsKey(key))
            {
                var saleInfo = saleInfos[key];
                var price = prices.ContainsKey(saleInfo.TokenAddress) ? prices[saleInfo.TokenAddress] : 0;
                data.Add(CreateItem(saleInfo, userInfos[key], price));
            }
        }
        Data = data.OrderByDescending(x => x.AvailablePercent).OrderByDescending(x => x.AvailableUsdAmount).ToArray();
    }

    private DashboardDataItem CreateItem(SaleInfo saleInfo, UserVestingInfo userVestingInfo, decimal price)
    {
        int count = userVestingInfo.PortionWithdrawn.Length;
        long ClaimedPercent = 0;
        long AvailablePercent = 0;
        var unixTimeNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        for (int i = 0; i < count && saleInfo.VestingTimes[i] < unixTimeNow; i++)
        {
            var portion = saleInfo.VestingPortions[i];
            if (userVestingInfo.PortionWithdrawn[i])
            {
                ClaimedPercent += portion;
            }
            else
            {
                AvailablePercent += portion;
            }
        }
        var lockedPercent = saleInfo.VestingPortionPrecision - AvailablePercent - ClaimedPercent;
        var totalTokens = Nethereum.Util.UnitConversion.Convert.FromWei(userVestingInfo.TotalTokens, saleInfo.TokenDecimals);
        var availableUsdAmount = totalTokens * AvailablePercent / saleInfo.VestingPortionPrecision * price;
        var lockedUsdAmount = totalTokens * lockedPercent / saleInfo.VestingPortionPrecision * price;
        return new DashboardDataItem(
            saleInfo.Address,
            totalTokens,
            (decimal)ClaimedPercent / saleInfo.VestingPortionPrecision,
            (decimal)AvailablePercent / saleInfo.VestingPortionPrecision,
            availableUsdAmount,
            lockedUsdAmount);
    }
}
