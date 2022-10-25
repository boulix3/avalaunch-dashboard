
using Microsoft.AspNetCore.Mvc;
using AvalaunchDashboard.Shared;
using AvalaunchDashboard.Server.Services;

namespace AvalaunchDashboard.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserInfoController : ControllerBase
{
    private readonly FireStore _store;
    public UserInfoController(FireStore store)
    {
        this._store = store;
    }

    [HttpGet("{address}")]
    public async Task<UserData> Get(string address)
    {
        return await _store.GetUserData(address);
    }

    [HttpGet("importdata/{address}")]
    public async Task<UserData> ImportData(string address)
    {
        return await _store.ImportUserData(address);
    }
}
