using System.Net.Http.Json;
using Fluxor;

namespace AvalaunchDashboard.Client.Flux.Prices;
public class Effects : HttpEffect<State>
{
    public const string baseUrlFormat = "https://api.coingecko.com/api/v3/simple/token_price/avalanche?contract_addresses={0}&vs_currencies=usd";
    public Effects(HttpClient http, IState<State> state) : base(http, state)
    {
    }

    [EffectMethod]
    public async Task LoadPrices(Sales.Actions.DataLoaded action, IDispatcher dispatcher)
    {
        var addresses = action.Data.Items.Values.Select(x => x.TokenAddress);
        if (addresses == null || !addresses.Any())
        {
            return;
        }
        addresses = addresses.Append(State.AvaxContractAddress);
        var url = string.Format(baseUrlFormat, string.Join(",", addresses));
        var baseResult = await _http.GetFromJsonAsync<Dictionary<string, Dictionary<string, decimal>>>(url);
        var data = baseResult != null ?
            baseResult.ToDictionary(x => x.Key, x => x.Value.Values.First()) :
            new();
        dispatcher.Dispatch(new Notifications.Actions.Notify("Prices loaded from CoinGecko"));
        dispatcher.Dispatch(new Actions.DataLoaded(data));
    }
}