using Fluxor;

namespace AvalaunchDashboard.Client.Flux;

public abstract class HttpEffect<T>
{
    internal IState<T> _state;

    internal readonly HttpClient _http;
    public HttpEffect(HttpClient http, IState<T> state)
    {
        _http = http;
        _state = state;
    }
}