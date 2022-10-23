using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using AvalaunchDashboard.Client;
using AvalaunchDashboard.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/") };
// builder.Services.AddScoped(sp => httpClient);
var appState = await State.CreateAppState(httpClient);
builder.Services.AddSingleton(appState);
builder.Services.AddMudServices();
await builder.Build().RunAsync();
