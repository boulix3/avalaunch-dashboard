using AvalaunchDashboard.Shared;

namespace AvalaunchDashboard.Client.Flux.User.Actions;
public record TryChangeWalletAddress(string WalletAddress);
public record ChangeWalletAddress(string WalletAddress);
public record Load;
public record Refresh;
public record DataLoaded(UserData Data);