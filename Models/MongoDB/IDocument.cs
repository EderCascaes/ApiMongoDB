﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApiMongoDB.Models.MongoDB_g
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        DateTime CreatedAt { get; }
    }

    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
