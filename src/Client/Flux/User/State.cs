using AvalaunchDashboard.Shared;
using Fluxor;

namespace AvalaunchDashboard.Client.Flux.User;

[FeatureState]
public record State(string WalletAddress, UserData Data, bool Loading)
{
    private State() : this(string.Empty, new(), false) { }
}