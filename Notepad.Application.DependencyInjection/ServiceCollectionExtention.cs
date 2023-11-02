
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Notepad.Application.Common.Authentication;
using Notepad.Application.Common.Behaviors;
using Notepad.Application.Common.Mapper;
using Notepad.Application.Common.Repositories;
using Notepad.Application.Notes.Command.Create;

namespace Notepad.Application.DependencyInjection
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationMapper).Assembly);
            return services;
        }
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CreateNoteCommand).Assembly);
            services.AddMediatR(x=>
            {
                x.RegisterServicesFromAssemblyContaining<CreateNoteCommand>();
                x.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            return services;
        }
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddTransient<IIdentityProvider, IdentityProvider>();
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<UserRepository>();
            return services;
        }
    }
}
