using Edupocket.DAL.Contracts;
using Edupocket.DAL.Repositories;
using Edupocket.Infrastructure;
using Edupocket.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edupocket.DAL
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static void AddOtherService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WalletDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("WalletConnectionString")));
        }

    }
}
