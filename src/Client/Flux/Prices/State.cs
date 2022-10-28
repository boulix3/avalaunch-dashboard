using Fluxor;

namespace AvalaunchDashboard.Client.Flux.Prices;
[FeatureState]
public record State(Dictionary<string, decimal> Prices, bool Loading)
{
    private State() : this(new(), false) { }
    public decimal GetAvaxPrice()
    {
        if (Prices.ContainsKey(AvaxContractAddress))
        {
            return Prices[AvaxContractAddress];
        }
        return 0;
    }
    public const string AvaxContractAddress = "0xb31f66aa3c1e785363f0875a1b74e27b85fd66c7";

}