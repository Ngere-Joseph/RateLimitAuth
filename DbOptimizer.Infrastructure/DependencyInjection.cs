using DbOptimizer.Core.Interfaces;
using DbOptimizer.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DbOptimizer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            return services;
        }
    }
}
