using System.Net.Http.Json;
using Fluxor;
using MudBlazor;

namespace AvalaunchDashboard.Client.Flux.Notifications;
public class Effects
{
    private readonly ISnackbar _snackbar;

    public Effects(MudBlazor.ISnackbar snackbar)
    {
        _snackbar = snackbar;
        _snackbar.Configuration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomLeft;
    }

    [EffectMethod]
    public async Task Notify(Notifications.Actions.Notify action, IDispatcher dispatcher)
    {
        await Show(action.Message, Severity.Info);
    }


    [EffectMethod]
    public async Task Warn(Notifications.Actions.Warn action, IDispatcher dispatcher)
    {
        await Show(action.Message, Severity.Warning);
    }

    public async Task Show(string message, MudBlazor.Severity severity)
    {
        message = $"{DateTime.Now:T} - {message}";
        _snackbar.Add(message, MudBlazor.Severity.Warning);
        await Task.CompletedTask;
    }
}