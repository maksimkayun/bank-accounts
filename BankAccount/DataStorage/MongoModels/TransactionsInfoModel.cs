using MongoDB.Bson.Serialization.Attributes;

namespace BankAccount.DataStorage.MongoModels;

public class TransactionsInfoModel : Transaction
{
    [BsonElement("transactionsInfo")]
    public List<Transaction> TransactionsInfo { get; set; }
}