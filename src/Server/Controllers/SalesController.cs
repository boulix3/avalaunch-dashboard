
using Microsoft.AspNetCore.Mvc;
using AvalaunchDashboard.Shared;
using AvalaunchDashboard.Server.Services;

namespace AvalaunchDashboard.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly FireStore _store;
    public SalesController(FireStore store)
    {
        this._store = store;
    }

    [HttpGet]
    public async Task<IEnumerable<SaleInfo>> Get()
    {
        return await _store.GetSales();
    }

    [HttpDelete]
    public async Task DeleteData()
    {
        await _store.DeleteSalesData();
    }

    [HttpGet("importdata")]
    public async Task<IEnumerable<SaleInfo>> ImportData()
    {
        return await _store.ImportSalesData();
    }
}
