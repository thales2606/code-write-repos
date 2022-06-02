using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Code.Writer.Repos.Domain.Contracts.Domain.Entities
{
    public interface IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; }

        DateTime CreatedAt { get; }
    }
}
