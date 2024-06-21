using BaseLibrary.Entities;
using ClientLibrary.Services.Implementations;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

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
builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<SfDialogService>();
builder.Services.AddScoped<GetHttpClient>();
builder.Services.AddScoped<CustomHttpHandler>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<AllState>();
builder.Services.AddScoped<IGenericService<GeneralDepartment>, GenericService<GeneralDepartment>>();
builder.Services.AddScoped<IGenericService<Department>, GenericService<Department>>();
builder.Services.AddScoped<IGenericService<Branch>, GenericService<Branch>>();
builder.Services.AddScoped<IGenericService<Country>, GenericService<Country>>();
builder.Services.AddScoped<IGenericService<City>, GenericService<City>>();
builder.Services.AddScoped<IGenericService<Town>, GenericService<Town>>();
builder.Services.AddScoped<IGenericService<Employee>, GenericService<Employee>>();

await builder.Build().RunAsync();
