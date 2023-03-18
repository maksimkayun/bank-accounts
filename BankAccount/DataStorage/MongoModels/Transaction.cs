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
    
    [BsonElement("type")]
    [BsonRepresentation(BsonType.String)]
    public string Type { get; set; }
    
    [BsonElement("amount")]
    [BsonRepresentation(BsonType.Int32)]
    public int Amount { get; set; }
    
    [BsonElement("sender")]
    [BsonRepresentation(BsonType.Array)]
    public Array Sender { get; set; }
    
    [BsonElement("recipient")]
    [BsonRepresentation(BsonType.Array)]
    public Array Recipient { get; set; }
}