using Blazored.LocalStorage;
using ListGenius.Web.Components.Auth;
using ListGenius.Web.Components.ProductGroups;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("ApiListGenius", options =>
{
    options.BaseAddress = new Uri("https://localhost:7050/");
}).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddScoped<CustomHttpHandler>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductGroupService, ProductGroupService>();

await builder.Build().RunAsync();
