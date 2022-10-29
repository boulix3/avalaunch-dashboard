
using AvalaunchDashboard.Client.Shared;
using AvalaunchDashboard.Shared;
using Microsoft.AspNetCore.Components;

namespace AvalaunchDashboard.Client.Pages
{
    public partial class Index : CustomBaseComponent
    {
        public bool Loading => _state.Value.Loading;
        public Dictionary<string, SaleInfo> Sales => _saleState.Value.Sales.Items;
        DashboardData DashboardData => new DashboardData(Sales, _state.Value.Data.Items, _pricesState.Value.Prices);

        protected override void OnParametersSet()
        {
            Console.WriteLine("OnParameterSet " + AddressParameter);
            if (AddressParameter != null)
            {
                Address = AddressParameter;
            }
            base.OnParametersSet();
        }
        [Parameter]
        public string? AddressParameter { get; set; }
        public string Address
        {
            get
            {
                return _state.Value.WalletAddress;
            }
            set
            {
                _dispatcher.Dispatch(new Flux.User.Actions.ChangeWalletAddress(value));
            }
        }

        public string ValidateAddress(string item)
        {
            if (IsNotValidAddress)
                return "Please enter a valid c-chain address";
            return string.Empty;
        }
        public bool IsNotValidAddress => !Address.IsValidAddress();
        protected override void OnInitialized()
        {
            if (Address.IsValidAddress() && AddressParameter == null)
            {
                _dispatcher.Dispatch(Flux.User.Actions.Navigator.Wallet(Address));
            }
            base.OnInitialized();
        }
        public void LoadData()
        {
            _dispatcher.Dispatch(new Flux.User.Actions.Load());
        }
        public void RefreshData()
        {
            _dispatcher.Dispatch(new Flux.User.Actions.Refresh());
        }

        public string GetTokenSymbol(string key)
        {
            var saleInfo = Sales[key];
            return saleInfo.TokenSymbol;
        }
        public decimal ConvertToAvax(decimal usdAmount)
        {
            var avaxPrice = _pricesState.Value.GetAvaxPrice();
            if (avaxPrice == 0)
            {
                return 0;
            }
            return usdAmount / avaxPrice;
        }

        public decimal GetTokenPrice(DashboardDataItem userInfo)
        {
            var saleInfo = Sales[userInfo.SaleContractAddress];
            var tokenAddress = saleInfo.TokenAddress.ToLower();
            var prices = _pricesState.Value.Prices;
            if (prices.ContainsKey(tokenAddress))
            {
                return prices[tokenAddress];
            }
            return 0;
        }

    }
}