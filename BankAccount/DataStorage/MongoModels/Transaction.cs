using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BankAccount.DataStorage.MongoModels;

public class Transaction
{
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("date")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Date { get; set; }
    
    [BsonElement("amount")]
    [BsonRepresentation(BsonType.Int32)]
    public int Amount { get; set; }
    
    [BsonElement("sender_account_id")]
    public string SenderAccountId { get; set; }
    
    [BsonElement("recipient_account_id")]
    public string RecipientAccountId { get; set; }
}