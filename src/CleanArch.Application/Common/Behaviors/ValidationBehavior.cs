using FluentValidation;
using MediatR;

namespace CleanArch.Application.Common.Behaviors;

/// <summary>
/// Behavior pipeline class which is capable of validating requests.
/// </summary>
/// <typeparam name="TRequest">Type of the request made.</typeparam>
/// <typeparam name="TResponse">Type of the response to the request.</typeparam>
internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// List of validators which are used for request validation (if any).
    /// </summary>
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Constructor of the <see cref="ValidationBehavior{TRequest,TResponse}"/> class.
    /// </summary>
    /// <param name="validators">Collection of <see cref="IValidator{T}"/> instances used for request validation.</param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Fail quickly.
        if (!_validators.Any()) return await next();

        var validationResults =
            await Task.WhenAll(_validators.Select(v => v.ValidateAsync(request, cancellationToken)));

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (failures.Any()) throw new ValidationException(failures);

        return await next();
    }
}