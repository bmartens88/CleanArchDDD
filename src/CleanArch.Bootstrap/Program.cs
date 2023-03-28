using CleanArch.Api;

var application = new ApiStartup(args, (services, configuration) => { });

await application.RunApiAsync();