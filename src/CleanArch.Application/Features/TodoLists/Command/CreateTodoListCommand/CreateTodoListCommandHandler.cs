using CleanArch.Application.Common.Interfaces.Repositories;
using CleanArch.Domain.TodoListAggregate;
using CleanArch.Domain.TodoListAggregate.Entities;
using MediatR;

namespace CleanArch.Application.Features.TodoLists.Command.CreateTodoListCommand;

/// <summary>
/// Handler which handles a Command of type <see cref="CreateTodoListCommand"/>.
/// </summary>
internal sealed class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, TodoList>
{
    /// <summary>
    /// Reference to an instance of <see cref="ISpecRepository{T}"/>.
    /// </summary>
    private readonly ISpecRepository<TodoList> _repository;

    /// <summary>
    /// Constructor of the <see cref="CreateTodoListCommandHandler" /> Handler class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="ISpecRepository{T}"/> for database interaction.</param>
    public CreateTodoListCommandHandler(ISpecRepository<TodoList> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<TodoList> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        // TODO:
        // [] Use some type of Guard or Policy to guarantee no duplicate list(s).
        // [] Error handling in case of exception/duplication.
        var list = TodoList.Create(
            request.Name,
            request.Description,
            items: request.items?.ConvertAll(item => TodoItem.Create(
                item.Name,
                item.Description)));

        await _repository.AddAsync(list, cancellationToken);

        return list;
    }
}
