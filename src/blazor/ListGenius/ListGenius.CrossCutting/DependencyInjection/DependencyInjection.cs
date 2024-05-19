using ListGenius.Domain.Business.Implementations;
using ListGenius.Domain.Business.Interfaces;
using ListGenius.Domain.Repository.Generic;
using ListGenius.Domain.Repository.GroupRepo;
using ListGenius.Domain.Repository.ProductRepo;
using ListGenius.Domain.Repository.ProductsListRepo;
using ListGenius.Domain.Repository.SharedProductRepo;
using ListGenius.Domain.Repository.SubGroupRepo;
using ListGenius.Domain.Repository.UserRepo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Threading.RateLimiting;

namespace ListGenius.CrossCutting.DependencyInjection
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Add Repository Services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RepositoryServices(this IServiceCollection services)
        {
            // Repository Services
            services.AddScoped<IProductsListRepository, ProductsListRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<ISubGroupRepository, SubGroupRepository>();
            services.AddScoped<ISharedProductRepository, SharedProductRepository>();

            return services;
        }

        /// <summary>
        /// Add Business Services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection BusinessServices(this IServiceCollection services)
        {
            // Business Services
            services.AddScoped<IProductBusiness, ProductBusiness>();
            services.AddScoped<IProductsListBusiness, ProductsListBusiness>();
            services.AddScoped<ILoginBusiness, LoginBusiness>();
            services.AddScoped<IFileBusiness, FileBusiness>();
            services.AddScoped<IGroupBusiness, GroupBusiness>();
            services.AddScoped<ISubGroupBusiness, SubGroupBusiness>();
            services.AddScoped<ISharedProductBusiness, SharedProductBusiness>();

            return services;
        }

        /// <summary>
        /// Add Generic Repository Service.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection GenericRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            return services;
        }

        /// <summary>
        /// Add Swagger support.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection Swagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "List Genius Blazor API - JoaoDiasDev",
                        Version = "v1",
                        Description = "Make your shopping lists with ease!",
                        Contact =
                            new OpenApiContact { Name = "João Dias", Url = new Uri("https://github.com/joaodiasdev") },
                    });
            });
        }

        /// <summary>
        /// Add RateLimiting support, Using sliding window limiter.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RateLimit(this IServiceCollection services)
        {
            return services.AddRateLimiter(_ => _
            .AddSlidingWindowLimiter(policyName: "sliding", options =>
            {
                options.PermitLimit = 30;
                options.Window = TimeSpan.FromSeconds(15);
                options.SegmentsPerWindow = 15;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 10;
            }));
        }

        /// <summary>
        /// Add Cors support.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection Cors(this IServiceCollection services)
        {
            return services.AddCors(
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
        }
    }
}
