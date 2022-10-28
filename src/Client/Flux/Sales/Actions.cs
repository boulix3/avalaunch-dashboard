using AvalaunchDashboard.Shared;

namespace AvalaunchDashboard.Client.Flux.Sales.Actions;
public record Load;
public record Refresh;
public record DataLoaded(SaleData Data);