using BankAccount.DataStorage;
using BankAccount.Interfaces;
using BankAccount.Services;
using BankAccount.Settings;
using Microsoft.EntityFrameworkCore;

namespace BankAccount;

public static class StartupExtentions
{
    public static IServiceCollection ConfigureDatabaseConnection(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddOptions();
        if (config.GetValue<string>("DBType") == "MongoDB")
        {
            services.Configure<MongoDbSettings>(config.GetSection("MongoDatabase"));
            services.AddSingleton<BankAccountMongoContext>();
            services.AddSingleton<ICRUDService, BankAccountMongoService>();
        } 
        else if (config.GetValue<string>("DBType") == "Postgres")
        {
            var connectionString = config["NpgDatabase:ConnectionString"];
            services.AddDbContext<BankAccountPgContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<BankAccountPgContext>();
            services.AddScoped<ICRUDService, BankAccountPostgresService>();
        }
        else
        {
            throw new Exception("The configuration file is incorrectly configured");
        }

        return services;
    }
}