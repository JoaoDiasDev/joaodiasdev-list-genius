using ClientLibrary.Services.Implementations;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("SystemApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7162/");
}).AddHttpMessageHandler<CustomHttpHandler>();
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7162/") });
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
builder.Services.AddScoped<GetHttpClient>();
builder.Services.AddScoped<CustomHttpHandler>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<DepartmentState>();
await builder.Build().RunAsync();
