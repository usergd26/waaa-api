using Microsoft.Extensions.DependencyInjection;
using Waaa.Application.Interfaces;
using Waaa.Application.Services;
using Waaa.Domain.Interfaces;
using Waaa.Domain.Repositories;

namespace Waaa.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWebinarRegistrationService, WebinarRegistrationService>();
            services.AddScoped<IWebinarRegistrationRepository, WebinarRegistrationRepository>();
            return services;
        }
    }
}
