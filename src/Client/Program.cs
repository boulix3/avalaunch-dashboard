using AvalaunchDashboard.Client;
using AvalaunchDashboard.Client.Services;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/") };
// builder.Services.AddScoped(sp => httpClient);
var appState = await State.CreateAppState(httpClient);
builder.Services.AddSingleton(appState);
builder.Services.AddSingleton(httpClient);
var currentAssembly = typeof(Program).Assembly;
builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(currentAssembly);
    options.UseReduxDevTools(rdt =>
    {
        rdt.Name = "AvalaunchDashboard";
        rdt.EnableStackTrace();
    });
});
builder.Services.AddMudServices();
await builder.Build().RunAsync();
