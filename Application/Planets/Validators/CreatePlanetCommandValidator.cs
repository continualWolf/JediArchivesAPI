using FluentValidation;
using JediArchives.Application.Planets.Commands;

namespace JediArchives.Application.Planets.Validators;

public class CreatePlanetCommandValidator : AbstractValidator<CreatePlanetCommand> {
    public CreatePlanetCommandValidator() {
        // Example validations as i tests fluent validation and the pipeline
        RuleFor(x => x.Name).NotEmpty().MaximumLength(80).WithMessage("Planet name is required.");
        RuleFor(x => x.SystemEntityId).NotEmpty().GreaterThan(0).WithMessage("System ID is required.");
        RuleFor(x => x.OrbitX).NotEmpty().GreaterThan(0).WithMessage("Orbit X is required.");
        RuleFor(x => x.OrbitY).NotEmpty().GreaterThan(0).WithMessage("Orbit Y is required.");
        RuleFor(x => x.Allegiance).NotEmpty().WithMessage("Allegiance is required.");
        // Need to add a rule for obit x and y to say that it needs to be in the selected systems area
    }
}
