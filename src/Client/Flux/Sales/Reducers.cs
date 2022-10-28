using Fluxor;
namespace AvalaunchDashboard.Client.Flux.Sales;
public static class Reducers
{
    [ReducerMethod]
    public static State Load(State state, Actions.Load action) =>
        state with { Loading = true, Sales = new() };
    [ReducerMethod]
    public static State Refresh(State state, Actions.Refresh action) =>
        state with { Loading = true, Sales = new() };
    [ReducerMethod]
    public static State DataLoaded(State state, Actions.DataLoaded action) =>
        state with { Loading = false, Sales = action.Data };
}
