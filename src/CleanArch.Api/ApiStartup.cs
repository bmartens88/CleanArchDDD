namespace CleanArch.Api;

public sealed class ApiStartup
{
    private readonly WebApplicationBuilder _builder;

    public ApiStartup(string[] args, Action<IServiceCollection>? options = null)
    {
        _builder = WebApplication.CreateBuilder(args);

        options?.Invoke(_builder.Services);
    }

    public Task RunApiAsync()
    {
        var app = _builder.Build();

        app.UseExceptionHandler("/error");

        if (app.Environment.IsDevelopment())
        {
        }

        app.UseHttpsRedirection();

        return app.RunAsync();
    }
}