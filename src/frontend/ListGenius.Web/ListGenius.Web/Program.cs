using Blazored.LocalStorage;
using ListGenius.Web.Components.Auth;
using ListGenius.Web.Components.Products;
using ListGenius.Web.Util;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IProductsListService = ListGenius.Web.Components.ProductsLists.IProductsListService;
using ProductsListService = ListGenius.Web.Components.ProductsLists.ProductsListService;

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
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductsListService, ProductsListService>();

await builder.Build().RunAsync();
