using CleanArch.Api;
using CleanArch.Application;
using CleanArch.Infrastructure;

var application = new ApiStartup(args, (services, configuration) =>
{
    services
        .AddApplication()
        .AddInfrastructure(configuration);
});

await application.RunApiAsync();