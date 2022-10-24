using AvalaunchDashboard.Shared;

namespace AvalaunchDashboard.Client.Services;

public record UserInfoStateData(string Address, UserData Data, bool Loading);