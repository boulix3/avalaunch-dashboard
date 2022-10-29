using AvalaunchDashboard.Shared;

namespace AvalaunchDashboard.Client.Pages;
public record DashboardDataItem(
    string SaleContractAddress,
    decimal TotalTokens,
    decimal ClaimedPercent,
    decimal AvailablePercent,
    decimal AvailableUsdAmount,
    decimal LockedUsdAmount);
