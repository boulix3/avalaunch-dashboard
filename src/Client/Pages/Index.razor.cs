using System.ComponentModel;
using System.Linq;
using AvalaunchDashboard.Client.Services;
using AvalaunchDashboard.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AvalaunchDashboard.Client.Pages
{
    public partial class Index : ComponentBase
    {


        public UserInfoStateData Data => _state.UserInfoState.UserData;
        public Dictionary<string, SaleInfo> Sales => _state.SaleInfo.Data.Items.Items;
        DashboardData DashboardData { get; set; } = new DashboardData();
        private string? urlParameterAddress;
        [Parameter]
        public string UrlParameterAddress
        {
            get
            {
                return urlParameterAddress ?? string.Empty;
            }
            set
            {
                urlParameterAddress = value;
                address = value;
            }
        }
        private string? address;
        public string Address
        {
            get
            {
                return address ?? string.Empty;
            }
            set
            {
                address = value;
                if (value.IsValidAddress() && !value.Equals(UrlParameterAddress))
                {
                    _navigation.NavigateTo($"/dashboard/{value}");
                }
            }
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine("OnAfterRender");
            if (!Data.Loading && Address.IsValidAddress() && Data.Address != Address)
            {
                Console.WriteLine("OnAfterRender LoadData");
                await LoadData();
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        public async Task LoadData()
        {
            await _state.UserInfoState.Load(Address);
        }
        public async Task RefreshData()
        {
            await _state.UserInfoState.Refresh(Address);
        }
        protected override async Task OnInitializedAsync()
        {
            _state.StateChanged += BeforeStateHasChanged;
            await base.OnInitializedAsync();
        }

        public void BeforeStateHasChanged()
        {
            Console.WriteLine($"update dashboard data - prices : {_state.CoinGeckoPrices.Data.Count}", _state.CoinGeckoPrices.Data);
            DashboardData = new DashboardData(Sales, Data.Data.Items, _state.CoinGeckoPrices.Data);
            StateHasChanged();
        }
        public string GetTokenSymbol(string key)
        {
            var saleInfo = Sales[key];
            return saleInfo.TokenSymbol;
        }
        public decimal ConvertToAvax(decimal usdAmount)
        {
            var avaxPrice = _state.CoinGeckoPrices.GetAvaxPrice();
            if (avaxPrice == 0)
            {
                return 0;
            }
            return usdAmount / avaxPrice;
        }

        public decimal GetTokenPrice(DashboardDataItem userInfo){
            var saleInfo = Sales[userInfo.SaleContractAddress];
            var tokenAddress = saleInfo.TokenAddress.ToLower();
            var prices = _state.CoinGeckoPrices.Data;
            if(prices.ContainsKey(tokenAddress)){
                return prices[tokenAddress];
            }
            return 0;
        }

    }
}