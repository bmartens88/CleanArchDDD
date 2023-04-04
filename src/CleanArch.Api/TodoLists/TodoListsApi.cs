using CleanArch.Application.Features.TodoLists.Command.CreateTodoListCommand;
using CleanArch.Contracts.Requests;
using MediatR;
using CreateTodoItem = CleanArch.Application.Features.TodoLists.Command.CreateTodoListCommand.CreateTodoItem;

namespace CleanArch.Api.TodoLists;

public static class TodoListsApi
{
    public static RouteGroupBuilder MapTodoLists(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("todo-lists")
            .WithGroupName("Todo Lists");

        group.MapPost("", async (
            CreateTodoListRequest request,
            ISender mediator,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateTodoListCommand(
                request.Name,
                request.Description,
                request.Items?.ConvertAll(item => new CreateTodoItem(item.Name, item.Description)));

            var list = await mediator.Send(command, cancellationToken);

            return Results.Ok(list);
        });

        return group;
    }
}
