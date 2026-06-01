using FluentValidation;

namespace CleanArch.Application.Behaviors;

/// <summary>
/// A MediatR pipeline behavior that performs validation on the incoming request using
/// all registered FluentValidation validators before passing it to the next handler.
/// </summary>
/// <typeparam name="TRequest">The type of the MediatR request.</typeparam>
/// <typeparam name="TResponse">The type of the response returned by the request handler.</typeparam>
/// <param name="validators">A collection of FluentValidation validators that apply to <typeparamref name="TRequest"/>.</param>
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Asynchronously validates the incoming request using all available validators.
    /// If no validators are registered, the request passes through unchanged.
    /// If any validation errors occur, a <see cref="ValidationException"/> is thrown.
    /// </summary>
    /// <param name="request">The request to validate.</param>
    /// <param name="next">A delegate that invokes the next behavior or the actual request handler.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The response produced by the next step in the pipeline.</returns>
    /// <exception cref="ValidationException">Thrown when one or more validation failures are detected.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next(cancellationToken);

        var context = new ValidationContext<TRequest>(request);

        var failures = (await Task.WhenAll(
                validators.Select(v => v.ValidateAsync(context, cancellationToken))))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .ToList();

        if (failures.Count == 0)
            return await next(cancellationToken);

        throw new ValidationException(failures);
    }
}