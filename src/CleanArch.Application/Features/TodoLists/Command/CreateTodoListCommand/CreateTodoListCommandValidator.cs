using FluentValidation;

namespace CleanArch.Application.Features.TodoLists.Command.CreateTodoListCommand;

/// <summary>
/// Validator class for Command of type <see cref="CreateTodoListCommand"/>.
/// </summary>
public sealed class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
    /// <summary>
    /// Constructor of the <see cref="CreateTodoListCommandValidator"/> Validator class.
    /// </summary>
    public CreateTodoListCommandValidator()
    {
        RuleFor(tl => tl.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(tl => tl.Description)
            .NotEmpty()
            .MaximumLength(100);

        RuleForEach(tl => tl.items)
            .ChildRules(iv =>
            {
                iv.RuleFor(ti => ti.Name)
                    .NotEmpty()
                    .MaximumLength(100);

                iv.RuleFor(ti => ti.Description)
                    .NotEmpty()
                    .MaximumLength(100);
            });
    }
}
