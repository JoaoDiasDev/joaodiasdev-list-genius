using EvolveDb;
using JoaoDiasDev.ListGenius.Business.Implementations;
using JoaoDiasDev.ListGenius.Business.Interfaces;
using JoaoDiasDev.ListGenius.Configurations;
using JoaoDiasDev.ListGenius.Hypermedia.Enricher;
using JoaoDiasDev.ListGenius.Hypermedia.Filters;
using JoaoDiasDev.ListGenius.Model.Context;
using JoaoDiasDev.ListGenius.Repository.Generic;
using JoaoDiasDev.ListGenius.Repository.ProductListRepo;
using JoaoDiasDev.ListGenius.Repository.UserRepo;
using JoaoDiasDev.ListGenius.Services.Token;
using JoaoDiasDev.ListGenius.Services.Token.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddXmlSerializerFormatters();

// Adding Hyper media, HATEOAS Support
var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new ProductEnricher());
filterOptions.ContentResponseEnricherList.Add(new ProductListEnricher());
builder.Services.AddSingleton(filterOptions);

// Swagger
builder.Services
    .AddSwaggerGen(
        options =>
        {
            options.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Product List API - João Dias Dev",
                    Version = "v1",
                    Description = "NET 8.0 Web RESTFul API",
                    Contact =
                        new OpenApiContact { Name = "João Dias", Url = new Uri("https://github.com/joaodiasdev") },
                });
        });

// Cors
builder.Services
    .AddCors(
        options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
        });

builder.Services.AddApiVersioning();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Business Services
//builder.Services.AddScoped<IProductBusiness, ProductBusiness>();
//builder.Services.AddScoped<IProductsListBusiness, ProductsListBusiness>();
builder.Services.AddScoped<ILoginBusiness, LoginBusiness>();
builder.Services.AddScoped<IFileBusiness, FileBusiness>();

// Repository Services
builder.Services.AddScoped<IProductsListRepository, ProductsListRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

// Token Services
builder.Services.AddSingleton<ITokenService, TokenService>();
var tokenConfiguration = new TokenConfiguration();

new ConfigureFromConfigurationOptions<TokenConfiguration>(builder.Configuration.GetSection("TokenConfigurations"))
    .Configure(tokenConfiguration);

builder.Services.AddSingleton(tokenConfiguration);

// Authentication
builder.Services
    .AddAuthentication(
        options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
    .AddJwtBearer(
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenConfiguration.Secret)),
                ValidIssuer = tokenConfiguration.Issuer,
                ValidAudience = tokenConfiguration.Audience,
            };
        });

// Authorization
builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build());

Log.Logger = new LoggerConfiguration()
    .WriteTo
    .Console()
    .CreateLogger();

// Database Context
var connection = builder.Configuration.GetSection("MySQLConnection")["MySQLConnectionString"];
builder.Services
    .AddDbContext<MySQLContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("Fixed", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 100;
    });
});

//Using Evolve to create and update database if not using docker
void MigrateDatabase(string connection)
{
    try
    {
        var evolveConnection = new MySqlConnection(connection);
        var evolve = new Evolve(evolveConnection, msg => Log.Information(msg))
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true
        };
        evolve.Migrate();
    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed", ex);
        throw;
    }
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // If needed to use Evolve to migration, instead docker
    MigrateDatabase(connection);
    app.UseSwagger();
    app.UseSwaggerUI(
        c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "RESTful API João Dias V1");
        });
};


app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

app.UseRouting();

app.UseCors();

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "DefaultApi",
    pattern: "{controller=values}/{id?}");

app.Run();
