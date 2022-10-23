using System.Net.Http.Json;
using AvalaunchDashboard.Shared;

namespace AvalaunchDashboard.Client.Services
{
    public class SaleInfoState : State
    {
        public SaleInfoStateData Data { get; set; }
        public SaleInfoState(HttpClient http) : base(http)
        {
            Data = new SaleInfoStateData(new SaleInfo[] { }, false);
        }
        public override async Task Initialize()
        {
            await LoadSaleInfo();
            await base.Initialize();
        }

        public async Task LoadSaleInfo()
        {
            Data = new SaleInfoStateData(new SaleInfo[] { }, true);
            StateHasChanged();
            var sales = await Http.GetFromJsonAsync<SaleInfo[]>("sales");
            sales = sales ?? new SaleInfo[] { };
            Data = new SaleInfoStateData(sales, false);
            StateHasChanged();
        }

        public async Task RefreshData()
        {
            Data = new SaleInfoStateData(Data.Items, true);
            StateHasChanged();
            await Http.DeleteAsync("sales");
            Data = new SaleInfoStateData(new SaleInfo[0], true);
            StateHasChanged();
            var sales = await Http.GetFromJsonAsync<SaleInfo[]>("sales/importdata");
            sales = sales ?? new SaleInfo[] { };
            Data = new SaleInfoStateData(sales, false);
            StateHasChanged();
        }
    }
}