using MongoDB.Bson.Serialization.Attributes;

namespace BankAccount.DataStorage.MongoModels;

[BsonIgnoreExtraElements]
public class TransactionsInfoModel : Account
{
    
    [BsonElement("outgoing_transactions_info")]
    public List<Transaction> OutgoingTransactionsInfo { get; set; }
    
    [BsonElement("incoming_transactions_info")]
    public List<Transaction> IncomingTransactionsInfo { get; set; }
}