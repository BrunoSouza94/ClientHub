using ClientHub.Application.Abstractions.Data;
using ClientHub.Domain.Abstractions;
using ClientHub.Domain.Entities.Addresses;
using ClientHub.Domain.Entities.Clients;
using ClientHub.Infrastructure.Data;
using ClientHub.Infrastructure.Image;
using ClientHub.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClientHub.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionsString = configuration.GetConnectionString("Database") ??
                            throw new ArgumentNullException(nameof(configuration));

        var redisConnectionsString = configuration.GetConnectionString("Cache") ??
                            throw new ArgumentNullException(nameof(configuration));

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<IImageService, ImageService>();

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(dbConnectionsString));

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnectionsString));

        services.AddStackExchangeRedisCache(options => options.Configuration = redisConnectionsString);

        services.Configure<AzureOptions>(configuration.GetSection("Azure"));

        return services;
    }
}