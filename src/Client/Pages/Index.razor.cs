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
        public string Address { get; set; }
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
            _state.UserInfoState.StateChanged += StateHasChanged;
            if (Address != null && Data.Data.LastUpdated < DateTimeOffset.UtcNow.AddMinutes(-10).ToUnixTimeSeconds())
            {
                await _state.UserInfoState.Load(Address);
            }
            await base.OnInitializedAsync();
        }

        public string ClaimedInfo(KeyValuePair<string, UserVestingInfo> info)
        {
            var claimed = info.Value.PortionWithdrawn.Count(x => x);
            var total = info.Value.PortionWithdrawn.Length;
            return $"{claimed}/{total}";
        }

    }
}