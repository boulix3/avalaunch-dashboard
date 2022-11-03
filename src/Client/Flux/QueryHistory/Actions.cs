using AvalaunchDashboard.Shared;

namespace AvalaunchDashboard.Client.Flux.QueryHistory.Actions;
public record Load;
public record DataLoaded(Dictionary<string, UserData> Data);
