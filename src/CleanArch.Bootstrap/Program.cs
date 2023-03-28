using CleanArch.Api;

var application = new ApiStartup(args, (services) => { });

await application.RunApiAsync();