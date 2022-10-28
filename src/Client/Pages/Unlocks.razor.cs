using AvalaunchDashboard.Client.Shared;
namespace AvalaunchDashboard.Client.Pages;

public partial class Unlocks : CustomBaseComponent
{
    public Dictionary<DateTime, List<UnlockDataItem>> MonthlyData =>
        GetTimedData();
    public UnlockData Data => new UnlockData(_saleState.Value.Sales.Items);

    public Dictionary<DateTime, List<UnlockDataItem>> GetTimedData()
    {
        var result = new Dictionary<DateTime, List<UnlockDataItem>>();
        foreach (var item in Data.Items)
        {
            var key = new DateTime(item.Date.Year, item.Date.Month, 1);
            if (!result.ContainsKey(key))
            {
                result.Add(key, new());
            }
            result[key].Add(item);
        }
        return result;
    }
    public bool alreadyScrolled = false;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (!alreadyScrolled && Data.Items.Any())
        {
            alreadyScrolled = true;
            var id = DateTime.Now.ToString("MMMMyyyy");
            Console.WriteLine("scrollauto " + id);
            await Task.Delay(1000);
            await _scrollManager.ScrollToFragmentAsync(id, MudBlazor.ScrollBehavior.Smooth);
        }
    }

    public MudBlazor.Color GetColor(DateTime key)
    {
        var thisMonth = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, 1);
        if (key < thisMonth)
        {
            return MudBlazor.Color.Secondary;
        }
        else if (key == thisMonth)
        {
            return MudBlazor.Color.Primary;
        }
        return MudBlazor.Color.Success;
    }

    public string GetSubTitle(DateTime key)
    {
        var now = DateTime.Now;
        var result = (key.Year - now.Year) * 12;
        result += key.Month - now.Month;
        return result == 0 ?
            "this month" :
            result < 0 ?
                $"{-result} months ago" :
                $"in {result} months";
    }
}
