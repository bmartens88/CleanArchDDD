using Ardalis.GuardClauses;
using CleanArch.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddPersistence(connectionString);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, string? connectionString = null)
    {
        Guard.Against.NullOrEmpty(connectionString);
        services.AddDbContext<CleanArchDbContext>(opts => opts.UseSqlite(connectionString));
        return services;
    }
}
