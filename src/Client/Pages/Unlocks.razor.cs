using Microsoft.AspNetCore.Components;
using AvalaunchDashboard.Client.Services;
namespace AvalaunchDashboard.Client.Pages;

public partial class Unlocks : ComponentBase
{
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
        StateHasChanged();
    }
        
}
