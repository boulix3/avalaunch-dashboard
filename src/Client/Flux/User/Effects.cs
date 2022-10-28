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
    public async Task WalletChanged(Actions.ChangeWalletAddress action, IDispatcher dispatcher)
    {
        await Task.CompletedTask;
        if (action.WalletAddress.IsValidAddress() &&
            (!_state.Value.Data.Items.Any() ||
                action.WalletAddress != _state.Value.WalletAddress))
        {
            dispatcher.Dispatch(Flux.User.Actions.Navigator.Wallet(action.WalletAddress));
            dispatcher.Dispatch(new Actions.Load());
        }
    }

    [EffectMethod]
    public async Task Load(Actions.Load _, IDispatcher dispatcher)
    {
        var address = _state.Value.WalletAddress;
        if (!address.IsValidAddress())
        {
            dispatcher.Dispatch(new Notifications.Actions.Warn("Invalid wallet address"));
            return;
        }
        var userData = await _http.GetFromJsonAsync<UserData>($"userinfo/{address}");
        userData = userData ?? new();
        dispatcher.Dispatch(new Actions.DataLoaded(userData));
    }

    [EffectMethod]
    public async Task Refresh(Actions.Refresh _, IDispatcher dispatcher)
    {
        var address = _state.Value.WalletAddress;
        if (!address.IsValidAddress())
        {
            dispatcher.Dispatch(new Notifications.Actions.Warn("Invalid wallet address"));
            return;
        }
        var userData = await _http.GetFromJsonAsync<UserData>($"userinfo/importdata/{address}");
        userData = userData ?? new();
        dispatcher.Dispatch(new Actions.DataLoaded(userData));
    }
}