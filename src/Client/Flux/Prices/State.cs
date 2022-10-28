using Fluxor;

namespace AvalaunchDashboard.Client.Flux.Prices;
[FeatureState]
public record State(Dictionary<string, decimal> Prices, bool Loading)
{
    private State() : this(new(), false) { }
}