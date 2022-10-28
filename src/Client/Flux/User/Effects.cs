using System.Net.Http.Json;
using AvalaunchDashboard.Shared;
using Fluxor;

namespace AvalaunchDashboard.Client.Flux.User;

public class Effects : HttpEffect<State>
{
    public Effects(HttpClient http, IState<State> state) : base(http, state)
    {
    }


    [EffectMethod]
    public void WalletChange(Actions.TryChangeWalletAddress action, IDispatcher dispatcher)
    {
        if (action.WalletAddress.IsValidAddress())
        {
            dispatcher.Dispatch(new Actions.ChangeWalletAddress(action.WalletAddress));
        }
    }

    [EffectMethod]
    public void WalletChanged(Actions.ChangeWalletAddress action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new Actions.Load());
    }

    [EffectMethod]
    public async Task Load(Actions.Load _, IDispatcher dispatcher)
    {
        var address = _state.Value.WalletAddress;
        var userData = await _http.GetFromJsonAsync<UserData>($"userinfo/{address}");
        userData = userData ?? new();
        dispatcher.Dispatch(new Actions.DataLoaded(userData));
    }

    [EffectMethod]
    public async Task Refresh(Actions.Refresh _, IDispatcher dispatcher)
    {
        var address = _state.Value.WalletAddress;
        var userData = await _http.GetFromJsonAsync<UserData>($"userinfo/importdata/{address}");
        userData = userData ?? new();
        dispatcher.Dispatch(new Actions.DataLoaded(userData));
    }
}