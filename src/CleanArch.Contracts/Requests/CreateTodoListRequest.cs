using System.ComponentModel.DataAnnotations;

namespace CleanArch.Contracts.Requests;

public sealed record class CreateTodoListRequest(
    [Required] string Name,
    [Required] string Description,
    List<CreateTodoItem>? Items = null);

public sealed record class CreateTodoItem(
    [Required] string Name,
    [Required] string Description);