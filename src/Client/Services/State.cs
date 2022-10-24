using System.Net.Http.Json;

namespace AvalaunchDashboard.Client.Services;
public abstract class State
{
    public List<State> ChildStates { get; } = new();
    protected State(HttpClient http)
    {
        Http = http;
    }
    public HttpClient Http { get; }
    public void StateHasChanged(string message)
    {
        StateChangedMessage?.Invoke(message);
        StateChanged?.Invoke();
    }
    public event Action<string>? StateChangedMessage;
    public event Action? StateChanged;
    public virtual async Task Initialize()
    {
        foreach (var state in ChildStates)
        {
            state.StateChangedMessage += StateHasChanged;
            await state.Initialize();
        }
    }
    public static async Task<AppState> CreateAppState(HttpClient http)
    {
        var result = new AppState(http);
        await result.Initialize();
        return result;
    }
}
