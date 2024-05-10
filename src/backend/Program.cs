using JoaoDiasDev.ProductList.Business.Implementations;
using JoaoDiasDev.ProductList.Business.Interfaces;
using JoaoDiasDev.ProductList.Configurations;
using JoaoDiasDev.ProductList.Hypermedia.Enricher;
using JoaoDiasDev.ProductList.Hypermedia.Filters;
using JoaoDiasDev.ProductList.Model.Context;
using JoaoDiasDev.ProductList.Repository.Generic;
using JoaoDiasDev.ProductList.Repository.ProductRepo;
using JoaoDiasDev.ProductList.Repository.UserRepo;
using JoaoDiasDev.ProductList.Services.Token;
using JoaoDiasDev.ProductList.Services.Token.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddXmlSerializerFormatters();

// Adding Hyper media, HATEOAS Support
var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());
builder.Services.AddSingleton(filterOptions);

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

builder.Services.AddScoped<IPersonBusiness, PersonBusiness>();
builder.Services.AddScoped<IBookBusiness, BookBusiness>();
builder.Services.AddScoped<ILoginBusiness, LoginBusiness>();
builder.Services.AddScoped<IFileBusiness, FileBusiness>();

builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var tokenConfiguration = new TokenConfiguration();

new ConfigureFromConfigurationOptions<TokenConfiguration>(builder.Configuration.GetSection("TokenConfigurations"))
    .Configure(tokenConfiguration);

builder.Services.AddSingleton(tokenConfiguration);

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

var connection = builder.Configuration.GetSection("MySQLConnection")["MySQLConnectionString"];
builder.Services
    .AddDbContext<MySQLContext>(options => options.UseMySql(connection, MySqlServerVersion.AutoDetect(connection)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Using Evolve to create and update database if not using docker
// void MigrateDatabase(string? connection)
// {
//    try
//    {
//        var evolveConnection = new MySqlConnection(connection);
//        var evolve = new Evolve(evolveConnection, msg => Log.Information(msg))
//        {
//            Locations = new List<string> { "db/migrations", "db/dataset" },
//            IsEraseDisabled = true
//        };
//        evolve.Migrate();
//    }
//    catch (Exception ex)
//    {
//        Log.Error("Database migration failed", ex);
//        throw;
//    }
// }
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // MigrateDatabase(connection);
    app.UseSwagger();
    app.UseSwaggerUI(
        c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "RESTful API João Dias V1");
        });
};

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "DefaultApi",
    pattern: "{controller=values}/{id?}");

app.Run();
