using Microsoft.AspNetCore.Identity;
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
            //Services
            services.AddScoped<IWebinarService, WebinarService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBluePrintService, BluePrintService>();
            services.AddTransient<IEmailSender<IdentityUser>, AuthEmailService>();

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWebinarRepository, WebinarRepository>();
            services.AddScoped<IBluePrintRepository, BluePrintRepository>();
            return services;
        }
    }
}
