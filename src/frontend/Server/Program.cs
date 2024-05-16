using Radzen;
using JoaoDiasDev.ListGenius.UI.Server.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents().AddHubOptions(options => options.MaximumReceiveMessageSize = 10 * 1024 * 1024).AddInteractiveWebAssemblyComponents();
builder.Services.AddControllers();
builder.Services.AddRadzenComponents();
builder.Services.AddHttpClient();
builder.Services.AddLocalization();
builder.Services.AddScoped<JoaoDiasDev.ListGenius.UI.Server.joaodiasdev_list_geniusService>();
builder.Services.AddDbContext<JoaoDiasDev.ListGenius.UI.Server.Data.joaodiasdev_list_geniusContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("joaodiasdev_list_geniusConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("joaodiasdev_list_geniusConnection")));
});
builder.Services.AddControllers().AddOData(opt =>
{
    var oDataBuilderjoaodiasdev_list_genius = new ODataConventionModelBuilder();
    oDataBuilderjoaodiasdev_list_genius.EntitySet<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group>("Groups");
    oDataBuilderjoaodiasdev_list_genius.EntitySet<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>("Products");
    oDataBuilderjoaodiasdev_list_genius.EntitySet<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList>("ProductsLists");
    oDataBuilderjoaodiasdev_list_genius.EntitySet<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>("SharedProducts");
    oDataBuilderjoaodiasdev_list_genius.EntitySet<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup>("SubGroups");
    oDataBuilderjoaodiasdev_list_genius.EntitySet<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>("Users");
    opt.AddRouteComponents("odata/joaodiasdev_list_genius", oDataBuilderjoaodiasdev_list_genius.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddScoped<JoaoDiasDev.ListGenius.UI.Client.joaodiasdev_list_geniusService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseRequestLocalization(options => options.AddSupportedCultures("en", "pt-BR").AddSupportedUICultures("en", "pt-BR").SetDefaultCulture("pt-BR"));
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode().AddInteractiveWebAssemblyRenderMode().AddAdditionalAssemblies(typeof(JoaoDiasDev.ListGenius.UI.Client._Imports).Assembly);
app.Run();