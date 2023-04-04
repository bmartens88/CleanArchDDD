using CleanArch.Api.TodoLists;
using CleanArch.Domain.TodoListAggregate.ValueObjects;

namespace CleanArch.Api;

public sealed class ApiStartup
{
    private readonly WebApplicationBuilder _builder;

    public ApiStartup(string[] args, Action<IServiceCollection, ConfigurationManager>? options = null)
    {
        _builder = WebApplication.CreateBuilder(args);

        options?.Invoke(_builder.Services, _builder.Configuration);
    }

    public Task RunApiAsync()
    {
        var app = _builder.Build();
        // app.Urls.Add("http://localhost:5000");
        // app.Urls.Add("https://localhost:5001");

        app.UseExceptionHandler("/error");

        if (app.Environment.IsDevelopment())
        {
            // Console.WriteLine("Running in development mode");
        }

        app.UseHttpsRedirection();

        app.MapTodoLists();

        return app.RunAsync();
    }
}