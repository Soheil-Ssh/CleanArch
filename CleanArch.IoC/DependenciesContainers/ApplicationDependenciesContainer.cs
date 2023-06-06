using CleanArch.Infrastructure.IRepositories.Common;
using CleanArch.Infrastructure.Repositories.Common;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.IoC.DependenciesContainers
{
    public static class ApplicationDependenciesContainer
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            #region Uow

            services.AddScoped<IUOW, UOW>();

            #endregion
            
            return services;
        }
    }
}
