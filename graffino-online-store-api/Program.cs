using FluentMigrator.Runner;
using graffino_online_store_api.Data;
using graffino_online_store_api.System.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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

        builder.Services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddMySql5()
                .WithGlobalConnectionString(builder.Configuration.GetConnectionString(SystemConstants.SQL_CONFIGURATION_NAME))
                .ScanIn(typeof(Program).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());
        
        #endregion
        
        #region AUTHORIZATION
        
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdmin", policy =>
            {
                policy.RequireRole("Admin");
            });
        });

        builder.Services.AddIdentityApiEndpoints<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();
        
        #endregion
        
        #region SERVICES
        
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
        app.UseAuthorization();
        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        app.UseCors(SystemConstants.CORS_CLIENT_POLICY_NAME);
        app.Run();

        #endregion
    }
}