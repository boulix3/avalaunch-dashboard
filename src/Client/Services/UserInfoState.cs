using System;
using System.Net.Http.Json;
using AvalaunchDashboard.Shared;

namespace AvalaunchDashboard.Client.Services;

public class UserInfoState : State
{
    public UserInfoStateData UserData { get; private set; }
    public UserInfoState(HttpClient http) : base(http)
    {
        UserData = new UserInfoStateData(string.Empty, new UserData(), false);
    }

    public async Task Load(string address)
    {
        UserData = UserData with { Address = address, Loading = true };
        StateHasChanged("Loading sale info");
        var userData = await Http.GetFromJsonAsync<UserData>($"userinfo/{address}");
        UserData = UserData with { Data = userData ?? UserData.Data, Loading = false };
        StateHasChanged("Sale info loaded");
    }
    public async Task Refresh(string address)
    {
        UserData = UserData with { Address = address, Loading = true };
        StateHasChanged("Deleting cached user info");
        await Http.DeleteAsync($"userinfo/{address}");
        UserData = UserData with { Data = new UserData() };
        StateHasChanged("Loading user info from blockchain and caching");
        var userData = await Http.GetFromJsonAsync<UserData>($"userinfo/importdata/{address}");
        UserData = UserData with { Data = userData ?? UserData.Data, Loading = false };
        StateHasChanged("User info refreshed");
    }
}

