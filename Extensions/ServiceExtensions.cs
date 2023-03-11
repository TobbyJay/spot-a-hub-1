using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Contracts;
using System;

namespace Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigurePostgreSQLContext(this IServiceCollection services,
       IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
            });
        }
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager,RepositoryManager>();
        }

    }
}
