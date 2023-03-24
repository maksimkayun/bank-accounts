using MongoDB.Bson.Serialization.Attributes;

namespace BankAccount.DataStorage.MongoModels;

public class TransactionsInfoModel : Account
{
    [BsonElement("transactionsInfo")]
    public List<Transaction> TransactionsInfo { get; set; }
}