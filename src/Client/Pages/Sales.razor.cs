using AvalaunchDashboard.Client.Services;
using AvalaunchDashboard.Shared;
using Microsoft.AspNetCore.Components;

namespace AvalaunchDashboard.Client.Pages
{
    public partial class Sales : ComponentBase
    {
        public string searchString = string.Empty;
        public SaleInfoStateData Data => _state.SaleInfo.Data;
        protected override async Task OnInitializedAsync()
        {
            _state.SaleInfo.StateChanged += StateHasChanged;
            // if (!Data.Loading && !Data.Items.Any())
            // {
            //     await _state.SaleInfo.LoadSaleInfo();
            // }
            await base.OnInitializedAsync();            
        }

        private bool Filter(SaleInfo element) => FilterFunc(element, searchString);

        private bool FilterFunc(SaleInfo element, string searchString)
        {            
            return 
                string.IsNullOrWhiteSpace(searchString) 
                || element.TokenName.Contains(searchString, StringComparison.OrdinalIgnoreCase) 
                || element.TokenSymbol.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                || element.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase);               
        }

        async Task RefreshData(){
await _state.SaleInfo.RefreshData();
        }

    }
}
