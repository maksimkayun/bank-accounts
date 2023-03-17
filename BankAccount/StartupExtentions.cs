using BankAccount.DataStorage;
using BankAccount.Interfaces;
using BankAccount.Services;
using BankAccount.Settings;

namespace BankAccount;

public static class StartupExtentions
{
    public static IServiceCollection ConfigureDatabaseConnection(this IServiceCollection services, ConfigurationManager config)
    {
        if (config.GetValue<string>("DBType") == "MongoDB")
        {
            services.Configure<MongoDbSettings>(config.GetSection("MongoDatabase"));
            services.AddSingleton<BankAccountMongoContext>();
            services.AddSingleton<ICRUDService, BankAccountMongoService>();
        } 
        else if (config.GetValue<string>("DBType") == "Postgres")
        {
    
        }
        else
        {
            throw new Exception("The configuration file is incorrectly configured");
        }

        return services;
    }
}