using Code.Writer.Repos.Domain.Contracts.Domain.Entities;
using MongoDB.Bson;

namespace Code.Writer.Repos.Domain.Entities
{
    public class BaseEntity : IEntity
    {
        public ObjectId Id { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public virtual void SetId(ObjectId id)
        {
            Id = id;
        }
        public virtual void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }
    }
}
