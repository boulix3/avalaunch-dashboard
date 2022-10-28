using System.Net.Http.Json;
using AvalaunchDashboard.Shared;
using Fluxor;

namespace AvalaunchDashboard.Client.Flux.Sales;

public class Effects : HttpEffect<State>
{
    public Effects(HttpClient http, IState<State> state) : base(http, state)
    {
    }

    [EffectMethod]
    public async Task Refresh(Actions.Refresh action, IDispatcher dispatcher)
    {
        var sales = await _http.GetFromJsonAsync<SaleData>("sales/importdata");
        sales = sales ?? new();
        dispatcher.Dispatch(new Actions.DataLoaded(sales));
    }

    [EffectMethod]
    public async Task Load(Actions.Load action, IDispatcher dispatcher)
    {
        var sales = await _http.GetFromJsonAsync<SaleData>("sales");
        sales = sales ?? new();
        dispatcher.Dispatch(new Actions.DataLoaded(sales));
    }
    [EffectMethod]
    public async Task DataLoaded(Actions.DataLoaded action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new Notifications.Actions.Notify("Sales data loaded"));
        await Task.CompletedTask;
    }
}