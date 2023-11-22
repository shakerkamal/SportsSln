using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(
                    options => options.UseSqlServer(
                        configuration.GetConnectionString("SportsStoreDB")));
        }
    }
}
