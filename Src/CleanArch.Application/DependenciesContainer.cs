using CleanArch.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Application;

public static class DependenciesContainer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // add MediatR
        services.AddMediatR(options =>
            options.RegisterServicesFromAssemblyContaining(typeof(DependenciesContainer)));

        // add Auto mapper
        services.AddAutoMapper(cfg => { }, typeof(DependenciesContainer).Assembly);

        // add Fluent validation
        services.AddValidatorsFromAssembly(typeof(DependenciesContainer).Assembly);

        // add Validation behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}