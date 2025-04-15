using FluentValidation;
using MediatR;

namespace JediArchives.Helper;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull {
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        // If there are no validators then just run the handler
        if (!_validators.Any()) {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        // run the validators
        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        // Create a list of any errors
        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        // If there are any errors throw an exception
        if (failures.Count != 0){
            throw new ValidationException(failures);
        }

        // There are no errors, run the handler
        return await next();
    }
}
