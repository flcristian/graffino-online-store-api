using graffino_online_store_api.Data;
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
        
        builder.Services.AddControllers();
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
            options.AddPolicy(SystemConstants.CORS_CLIENT_POLICY_NAME, domain => domain.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
            );
        }); 
        
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(builder.Configuration.GetConnectionString(SystemConstants.SQL_CONFIGURATION_NAME)!,
                new MySqlServerVersion(new Version(8, 0, 21))));
        
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
        
        // MISCELLANEOUS
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        #endregion
        
        #region APPLICATION BUILD

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapIdentityApi<IdentityUser>();
        app.UseHttpsRedirection();
        app.UseCors(SystemConstants.CORS_CLIENT_POLICY_NAME);
        app.UseAuthorization();
        app.UseRouting();
        app.MapControllers();
        app.Run();

        #endregion
    }
}