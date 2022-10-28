using AvalaunchDashboard.Shared;

namespace AvalaunchDashboard.Client.Flux.User.Actions;
public record ChangeWalletAddress(string WalletAddress);
public record Load;
public record Refresh;
public record DataLoaded(UserData Data);

public static class Navigator
{
    public static Fluxor.Blazor.Web.Middlewares.Routing.GoAction Wallet(string walletAddress)
    {
        return new Fluxor.Blazor.Web.Middlewares.Routing.GoAction($"dashboard/{walletAddress}");
    }
}