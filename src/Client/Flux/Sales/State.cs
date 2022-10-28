using AvalaunchDashboard.Shared;
using Fluxor;

namespace AvalaunchDashboard.Client.Flux.Sales;

[FeatureState]
public record State(SaleData Sales, bool Loading)
{
    private State() : this(new(), false) { }
}