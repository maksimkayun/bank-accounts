﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BankAccount.DataStorage.MongoModels;

public class Account
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("account_number")]
    [BsonRepresentation(BsonType.String)]
    public string AccountNumber { get; set; }
    
    [BsonElement("balance")]
    [BsonRepresentation(BsonType.Int32)]
    public int Balance { get; set; }
    
    [BsonElement("opening_date")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime OpeningDate { get; set; }
    
    [BsonElement("closing_date")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime ClosingDate { get; set; }
    
    [BsonElement("owner")]
    [BsonRepresentation(BsonType.Array)]
    public Array Owner { get; set; }
}