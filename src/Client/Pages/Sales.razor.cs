
using AvalaunchDashboard.Client.Shared;
using AvalaunchDashboard.Shared;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AvalaunchDashboard.Client.Pages
{
    public partial class Sales : CustomBaseComponent
    {
        public string searchString = string.Empty;
        public SaleInfo[] Items => _state.Value.Sales.Items.Values.ToArray();
        private bool Filter(SaleInfo element) => FilterFunc(element, searchString);
        private bool FilterFunc(SaleInfo element, string searchString)
        {
            return
                string.IsNullOrWhiteSpace(searchString)
                || element.TokenName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                || element.TokenSymbol.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                || element.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase);
        }

        public void RefreshData()
        {

            _dispatcher.Dispatch(new Flux.Sales.Actions.Refresh());
        }

        public string VestingInfo(SaleInfo saleInfo)
        {
            var dates = saleInfo.VestingTimes.Select(x => x.ToDateTimeOffset());
            var now = DateTimeOffset.UtcNow;
            var passed = dates.Count(x => x < now);
            return $"{passed}/{dates.Count()}";
        }

        public void ShowVestingInfo(SaleInfo saleInfo)
        {
            var parameters = new DialogParameters();
            parameters.Add("Sale", saleInfo);
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraExtraLarge };
            DialogService.Show<Shared.VestingInfo>($"{saleInfo.TokenSymbol} vesting info", parameters, options);
        }

        public string SnowtraceContractLink(string address)
        {
            return $"https://snowtrace.io/address/{address}#code";
        }

        public string GetTokenPrice(string tokenAddress)
        {
            var price = _pricesState.Value.Prices.ContainsKey(tokenAddress) ?
                _pricesState.Value.Prices[tokenAddress] :
                0;
            return "$" + price.ToString("N5");
        }
    }
}
