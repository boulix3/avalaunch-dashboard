using AvalaunchDashboard.Shared;
using Fluxor;

namespace AvalaunchDashboard.Client.Flux.QueryHistory;

[FeatureState]
public record State(string WalletAddress, Dictionary<string, UserData> Data, bool Loading)
{
    private State() : this(string.Empty, new(), false) { }
}