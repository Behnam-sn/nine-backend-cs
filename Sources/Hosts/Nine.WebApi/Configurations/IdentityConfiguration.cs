using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nine.Identity.Domain.Contracts.Users.ValueObjects;
using Nine.Identity.Domain.Users.Entities;
using Nine.Identity.Domain.Users.UnitOfWorks;
using Nine.Identity.Infrastructure.DbContexts;
using Nine.Identity.Infrastructure.UnitOfWorks;
using Nine.Identity.Presentation.Users.ExceptionHandlers;

namespace Nine.WebApi.Configurations;

public static class IdentityConfiguration
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        AddInfrastructure(services);
        AddPresentation(services);

        return services;
    }

    public static async Task<WebApplication> ApplyIdentityMigrationsAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
        await dbContext.Database.MigrateAsync();
        return app;
    }

    public static async Task<WebApplication> AddIdentityRolesAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<UserId>>>();

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole<UserId>("Admin") { Id = UserId.Create() });
        }

        if (!await roleManager.RoleExistsAsync("Member"))
        {
            await roleManager.CreateAsync(new IdentityRole<UserId>("Member") { Id = UserId.Create() });
        }

        return app;
    }

    private static void AddInfrastructure(IServiceCollection services)
    {
        services.AddDbContext<ApplicationIdentityDbContext>((serviceProvider, options) =>
        {
            var connectionString = serviceProvider.GetRequiredService<IConfiguration>()
                .GetConnectionString("Identity");
            ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

            options.UseNpgsql(
                connectionString,
                npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "identity"));
        });

        services.AddAuthorization();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddPresentation(IServiceCollection services)
    {
        services.AddExceptionHandler<AuthExceptionHandler>();
        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole<UserId>>()
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>();
    }
}
