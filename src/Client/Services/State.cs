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
    public void StateHasChanged()
    {
        StateChanged?.Invoke();
    }
    public event Action? StateChanged;
    public virtual async Task Initialize()
    {
        foreach (var state in ChildStates)
        {
            state.StateChanged += StateHasChanged;
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
