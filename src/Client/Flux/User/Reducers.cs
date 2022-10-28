using Fluxor;
namespace AvalaunchDashboard.Client.Flux.User;
public static class Reducers
{

    [ReducerMethod]
    public static State Load(State state, Actions.Load action) =>
        state with { Loading = true, Data = new() };
    [ReducerMethod]
    public static State Refresh(State state, Actions.Refresh action) =>
        state with { Loading = true, Data = new() };
    [ReducerMethod]
    public static State DataLoaded(State state, Actions.DataLoaded action) =>
        state with { Loading = false, Data = action.Data };
    [ReducerMethod]
    public static State ValidWalletChanged(State state, Actions.ChangeWalletAddress action) =>
        state with { WalletAddress = action.WalletAddress };
}
