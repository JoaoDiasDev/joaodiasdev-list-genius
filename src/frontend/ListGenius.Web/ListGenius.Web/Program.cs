using Blazored.LocalStorage;
using ListGenius.Web.Components.Auth;
using ListGenius.Web.Components.Products;
using ListGenius.Web.Util;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using IProductsListService = ListGenius.Web.Components.ProductsLists.IProductsListService;
using ProductsListService = ListGenius.Web.Components.ProductsLists.ProductsListService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HTTP client with a custom handler
builder.Services.AddHttpClient("ApiListGenius", options =>
{
    options.BaseAddress = new Uri("https://localhost:7050/");
}).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddScoped<CustomHttpHandler>();

// Add Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Add MudBlazor Services
builder.Services.AddMudServices();

// Add Authorization Core
builder.Services.AddAuthorizationCore();

// Add Authentication State Provider
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

// Add AuthService and other services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductsListService, ProductsListService>();

// Add CascadingAuthenticationState
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

await builder.Build().RunAsync();