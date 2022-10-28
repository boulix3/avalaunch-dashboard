using Fluxor;

namespace AvalaunchDashboard.Client.Flux.Prices;
public class Reducers
{
    [ReducerMethod]
    public static State Load(State state, Actions.Load action) =>
           state with { Loading = true, Prices = new() };
    [ReducerMethod]
    public static State DataLoaded(State state, Actions.DataLoaded action) =>
        state with { Loading = false, Prices = action.Data };
}