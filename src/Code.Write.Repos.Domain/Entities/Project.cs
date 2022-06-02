using Code.Writer.Repos.Domain.Contracts.Attributes;

namespace Code.Writer.Repos.Domain.Entities
{
    [BsonCollection("projects")]
    public class Project : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IEnumerable<Repository>? Repositories { get; set; }
    }
}
