
using AvalaunchDashboard.Server.Services;
using AvalaunchDashboard.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AvalaunchDashboard.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoryController : ControllerBase
{
    private readonly FireStore _store;
    public HistoryController(FireStore store)
    {
        this._store = store;
    }
    [HttpGet]
    public async Task<Dictionary<string, UserData>> History()
    {
        return await _store.UserHistory();
    }
}
