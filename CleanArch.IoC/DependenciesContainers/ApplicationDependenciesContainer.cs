using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.IoC.DependenciesContainers
{
    public static class ApplicationDependenciesContainer
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {

            return services;
        }
    }
}
