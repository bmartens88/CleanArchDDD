using CleanArch.Api;
using CleanArch.Infrastructure;

var application = new ApiStartup(args, (services, configuration) =>
{
    services.AddInfrastructure(configuration);
});

await application.RunApiAsync();