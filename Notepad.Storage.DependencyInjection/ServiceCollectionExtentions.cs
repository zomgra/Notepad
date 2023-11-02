using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Notepad.Storage.DependencyInjection
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddNoteDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<NoteDbContext>(x=>x.UseNpgsql(configuration.GetConnectionString("Postgresql")));
            services.AddDbContext<NoteDbContext>(x=>x.UseSqlite(configuration.GetConnectionString("SQLite")));
            return services;
        }
    }
}
