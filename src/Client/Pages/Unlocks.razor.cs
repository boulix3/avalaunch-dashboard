using Microsoft.AspNetCore.Components;
using AvalaunchDashboard.Client.Services;
namespace AvalaunchDashboard.Client.Pages;

public partial class Unlocks : ComponentBase
{
    public Dictionary<string, List<UnlockDataItem>> MonthlyData { get; set; } = new Dictionary<string, List<UnlockDataItem>>();
    public UnlockData Data { get; private set; } = new();

    protected override async Task OnInitializedAsync()
    {
        _state.StateChanged += BeforeStateHasChanged;
        await base.OnInitializedAsync();
    }

    public void BeforeStateHasChanged()
    {
        Console.WriteLine($"update dashboard data - prices : {_state.CoinGeckoPrices.Data.Count}", _state.CoinGeckoPrices.Data);
        Data = new UnlockData(_state.SaleInfo.Data.Items.Items);
        UpdateTimedData();
        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            var id = DateTime.Now.ToString("MMMMyyyy");
            Console.WriteLine("scrollauto " + id);
            await Task.Delay(2000);
            await _scrollManager.ScrollToFragmentAsync(id, MudBlazor.ScrollBehavior.Smooth);
        }
    }

    public void UpdateTimedData()
    {
        MonthlyData.Clear();
        foreach (var item in Data.Items)
        {
            var key = item.Date.ToString("MMMM yyyy");
            if (!MonthlyData.ContainsKey(key))
            {
                MonthlyData.Add(key, new());
            }
            MonthlyData[key].Add(item);
        }
    }    
}
