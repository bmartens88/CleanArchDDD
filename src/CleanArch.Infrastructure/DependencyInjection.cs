using Ardalis.GuardClauses;
using CleanArch.Application.Common.Interfaces.Repositories;
using CleanArch.Infrastructure.Events.Dispatcher;
using CleanArch.Infrastructure.Events.Interfaces;
using CleanArch.Infrastructure.Persistence;
using CleanArch.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IEventDispatcher, DomainEventDispatcher>();

        var connectionString = configuration.GetConnectionString("Default");
        services.AddPersistence(connectionString);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, string? connectionString = null)
    {
        Guard.Against.NullOrEmpty(connectionString);
        services.AddDbContext<CleanArchDbContext>(opts => opts.UseSqlite(connectionString));
        services.AddScoped(typeof(ISpecRepository<>), typeof(SpecRepository<>));
        return services;
    }
}
