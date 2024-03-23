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
            services.AddScoped<BankAccountMongoContext>();
            services.AddScoped<IClientService, BankAccountMongoService>();
            services.AddScoped<ITransactionsService, BankAccountMongoService>();
            services.AddScoped<IAccountService, BankAccountMongoService>();
            services.AddScoped<ITechnicalSupport, TechnicalMongoSupportService>();
        } 
        else if (config.GetValue<string>("DBType") == "Postgres")
        {
            var connectionString = config["NpgDatabase:ConnectionString"];
            services.AddDbContext<BankAccountPgContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.EnableSensitiveDataLogging();
            });
            services.AddScoped<BankAccountPgContext>();
            services.AddScoped<IClientService, BankAccountPostgresService>();
            services.AddScoped<ITransactionsService, BankAccountPostgresService>();
            services.AddScoped<IAccountService, BankAccountPostgresService>();
            services.AddScoped<ITechnicalSupport, TechnicalPostgresSupportService>();
            //EntityFrameworkProfiler.InitializeOfflineProfiling($"log_{DateTime.Now.ToLocalTime().ToString().Replace(".", "_").Replace(":", "_")}.EFProf");
            //EntityFrameworkProfiler.Initialize();
        }
        else
        {
            throw new Exception("The configuration file is incorrectly configured");
        }


        return services;
    }
}