using System.Net.Http.Json;
using AvalaunchDashboard.Shared;
using Fluxor;

namespace AvalaunchDashboard.Client.Flux.QueryHistory;

public class Effects : HttpEffect<State>
{
    public Effects(HttpClient http, IState<State> state) : base(http, state)
    {
    }

    [EffectMethod]
    public async Task Load(Actions.Load _, IDispatcher dispatcher)
    {
        var historyData = await _http.GetFromJsonAsync<Dictionary<string, UserData>>($"history");
        historyData = historyData ?? new();
        dispatcher.Dispatch(new Actions.DataLoaded(historyData));
    }
}