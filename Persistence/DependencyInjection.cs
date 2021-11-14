using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Abstractions;
using Persistence.Configuration;
using Persistence.Implimentations;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbOptions>(configuration.GetSection("MongoDb"));
            services.AddSingleton<IRepository<Student>, MongoDbRepository<Student>>();
            services.AddSingleton<MongoDbConfiguration>();

            return services;
        }
    }
}
