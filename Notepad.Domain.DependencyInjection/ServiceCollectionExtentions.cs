using Microsoft.Extensions.DependencyInjection;
using Notepad.Domain.Authentication;

namespace Notepad.Domain.DependencyInjection
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddTransient<IIdentityProvider, IdentityProvider>();
            return services;
        }
    }
}
