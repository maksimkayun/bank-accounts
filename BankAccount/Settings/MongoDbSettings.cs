namespace BankAccount.Settings;

public class MongoDbSettings
{
    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }

    public List<string> CollectionNames { get; set; }
}