using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BankAccount.DataStorage.MongoModels;

public class Client
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("name")]
    [BsonRepresentation(BsonType.String)]
    public string Name { get; set; }
    
    [BsonElement("surname")]
    [BsonRepresentation(BsonType.String)]
    public string SurName { get; set; }
    
    [BsonElement("birthday_date")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Birthday { get; set; }
    
    [BsonElement("email")]
    [BsonRepresentation(BsonType.String)]
    public string Email { get; set; }
    
    [BsonElement("phone_number")]
    [BsonRepresentation(BsonType.String)]
    public string PhoneNumber { get; set; }
}