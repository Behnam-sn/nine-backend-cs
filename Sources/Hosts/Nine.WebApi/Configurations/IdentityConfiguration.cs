using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Nine.Identity.Domain.Users.Entities;
using Nine.Identity.Infrastructure.DbContexts;

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
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>("Admin") { Id = Guid.NewGuid() });
        }

        if (!await roleManager.RoleExistsAsync("Member"))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>("Member") { Id = Guid.NewGuid() });
        }

        return app;
    }

    public static async Task<WebApplication> AddIdentityRolesAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
        await dbContext.Database.MigrateAsync();
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
    }

    private static void AddPresentation(IServiceCollection services)
    {
        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>();
    }
}
