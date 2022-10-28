namespace AvalaunchDashboard.Client.Flux.Prices.Actions;
public record Load;
public record DataLoaded(Dictionary<string, decimal> Data);