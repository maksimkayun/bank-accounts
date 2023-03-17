using BankAccount.DataStorage.MongoModels;
using BankAccount.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BankAccount.DataStorage;

public class BankAccountMongoContext
{
    public IMongoCollection<Client> Clients { get; private set; }
    public IMongoCollection<Account> Accounts { get; private set; }
    public IMongoCollection<Transactions> Transactions { get; private set; }
    
    private IMongoDatabase _mongoDatabase;

    public BankAccountMongoContext(IOptions<MongoDbSettings> settings)
    {
        var mongoClient = new MongoClient(settings.Value.ConnectionString);

        _mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
        Clients = GetCollection<Client>(name: "clients", settings: settings);
        Accounts = GetCollection<Account>(name: "accounts", settings: settings);
        Transactions = GetCollection<Transactions>(name: "transactions", settings: settings);
    }
    
    private IMongoCollection<T> GetCollection<T>(string name, IOptions<MongoDbSettings> settings) => _mongoDatabase.GetCollection<T>(
        settings.Value.CollectionNames.SingleOrDefault(e => e == name));
}