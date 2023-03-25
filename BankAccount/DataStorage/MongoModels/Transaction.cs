using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BankAccount.DataStorage.MongoModels;

public class Transaction
{
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("number_transaction")]
    [BsonRepresentation(BsonType.Int32)]
    public int NumberTransaction { get; set; }
    
    [BsonElement("date")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Date { get; set; }
    
    [BsonElement("amount")]
    [BsonRepresentation(BsonType.Int32)]
    public int Amount { get; set; }
    
    [BsonElement("sender_account_number")]
    [BsonRepresentation(BsonType.Int32)]
    public int SenderAccountNumber { get; set; }
    
    [BsonElement("recipient_account_number")]
    [BsonRepresentation(BsonType.Int32)]
    public int RecipientAccountNumber { get; set; }
}