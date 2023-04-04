using CleanArch.Application.Common.Interfaces.Repositories;
using CleanArch.Domain.TodoListAggregate;
using MediatR;

namespace CleanArch.Application.Features.TodoLists.Query.GetTodoListsQuery;

/// <summary>
/// Handler which handles a Query of type <see cref="GetTodoListsQuery"/>.
/// </summary>
internal sealed class GetTodoListsQueryHandler : IRequestHandler<GetTodoListsQuery, IReadOnlyList<TodoList>>
{
    /// <summary>
    /// Reference to an instance of <see cref="ISpecRepository{T}"/>.
    /// </summary>
    private readonly ISpecRepository<TodoList> _repository;

    /// <summary>
    /// Constructor of the <see cref="GetTodoListsQueryHandler"/> Handler class.
    /// </summary>
    /// <param name="repository">Instance of <see cref="ISpecRepository{T}"/> for database interaction.</param>
    public GetTodoListsQueryHandler(ISpecRepository<TodoList> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<TodoList>> Handle(GetTodoListsQuery _, CancellationToken cancellationToken)
    {
        var lists = await _repository.ListAsync(cancellationToken);

        return lists.AsReadOnly();
    }
}
