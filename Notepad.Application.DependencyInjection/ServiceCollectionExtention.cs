
using Microsoft.Extensions.DependencyInjection;
using Notepad.Application.Common.Mapper;
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
            services.AddMediatR(x=>
            {
                x.RegisterServicesFromAssembly(typeof(CreateNoteCommand).Assembly);
            });
            return services;
        }
    }
}
