using System.Net.Http.Json;
using AvalaunchDashboard.Shared;

namespace AvalaunchDashboard.Client.Services
{
    public class SaleInfoState : State
    {
        public SaleInfoStateData Data { get; private set; }
        public SaleInfoState(HttpClient http) : base(http)
        {
            Data = new SaleInfoStateData(new(), false);
        }
        public override async Task Initialize()
        {
            await Load();
            await base.Initialize();
        }

        public async Task Load()
        {
            Data = new SaleInfoStateData(new(), true);
            StateHasChanged("Loading sale info");
            var sales = await Http.GetFromJsonAsync<SaleData>("sales");
            sales = sales ?? new();
            Data = new SaleInfoStateData(sales, false);
            StateHasChanged("Sale info loaded");
        }

        public async Task Refresh()
        {
            Data = new SaleInfoStateData(Data.Items, true);
            StateHasChanged("Deleting cached sale info");
            await Http.DeleteAsync("sales");
            Data = new SaleInfoStateData(new(), true);
            StateHasChanged("Loading sale info from blockchain and caching");
            var sales = await Http.GetFromJsonAsync<SaleData>("sales/importdata");
            sales = sales ?? new();
            Data = new SaleInfoStateData(sales, false);
            StateHasChanged("Sale info refreshed");
        }
    }
}