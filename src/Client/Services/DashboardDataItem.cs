using AvalaunchDashboard.Shared;

namespace AvalaunchDashboard.Client.Services;
public record DashboardDataItem(
    string SaleContractAddress,
    decimal TotalTokens,
    decimal ClaimedPercent,
    decimal AvailablePercent,
    decimal AvailableUsdAmount);
