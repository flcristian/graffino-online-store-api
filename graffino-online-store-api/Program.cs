using System.Text.Json.Serialization;
using graffino_online_store_api.Data;
using graffino_online_store_api.Orders.Controllers;
using graffino_online_store_api.Orders.Controllers.Interfaces;
using graffino_online_store_api.Orders.Repository;
using graffino_online_store_api.Orders.Repository.Interfaces;
using graffino_online_store_api.Orders.Services;
using graffino_online_store_api.Orders.Services.Interfaces;
using graffino_online_store_api.Products.Repository;
using graffino_online_store_api.Products.Repository.Interfaces;
using graffino_online_store_api.Products.Services;
using graffino_online_store_api.Products.Services.Interfaces;
using graffino_online_store_api.System.Constants;
using graffino_online_store_api.Users.Repository;
using graffino_online_store_api.Users.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Stripe;
using Swashbuckle.AspNetCore.Filters;

namespace graffino_online_store_api;

internal class Program
{
    public static void Main(string[] args)
    {
        #region SETUP
        
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
        StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(SystemConstants.CORS_CLIENT_POLICY_NAME, domain => domain.WithOrigins("http://localhost:4200", "http://clientapp")
                .AllowAnyHeader()
                .AllowAnyMethod()
            );
        });

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(builder.Configuration.GetConnectionString(SystemConstants.SQL_CONFIGURATION_NAME)!,
                new MySqlServerVersion(new Version(8, 0, 21)),
                mySqlOptions => mySqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null)
            ));
        
        #endregion
        
        #region AUTHORIZATION
        
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.REQUIRE_ADMIN_POLICY, policy =>
            {
                policy.RequireRole("Administrator");
            });
        });

        builder.Services.AddIdentityApiEndpoints<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();
        
        #endregion
        
        #region SERVICES
        
        // REPOSITORIES
        builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
        builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
        builder.Services.AddScoped<IUsersRepository, UsersRepository>();
        
        // SERVICES
        builder.Services.AddScoped<IProductsQueryService, ProductsQueryService>();
        builder.Services.AddScoped<IProductsCommandService, ProductsCommandService>();
        builder.Services.AddScoped<IOrdersQueryService, OrdersQueryService>();
        builder.Services.AddScoped<IOrdersCommandService, OrdersCommandService>();

        builder.Services.AddScoped<OrdersApiController, OrdersController>();
        
        // MISCELLANEOUS
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        #endregion
        
        #region APPLICATION BUILD

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapIdentityApi<IdentityUser>();
        app.UseHttpsRedirection();
        app.UseCors(SystemConstants.CORS_CLIENT_POLICY_NAME);
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();

        #endregion
    }
}