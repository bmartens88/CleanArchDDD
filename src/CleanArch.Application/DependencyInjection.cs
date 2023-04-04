using CleanArch.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Application;

/// <summary>
/// Registers services for Dependency Injection (DI).
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Register Application services for DI.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> used for registering services to the DI container.</param>
    /// <returns>Configured DI container.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddMediatRApplicationServices()
            .AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }

    /// <summary>
    /// Registers services for MediatR to the DI container
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> used for registering services to the DI container.</param>
    /// <returns>Configured DI container.</returns>
    private static IServiceCollection AddMediatRApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            cfg.NotificationPublisher = new TaskWhenAllPublisher();
        })
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
