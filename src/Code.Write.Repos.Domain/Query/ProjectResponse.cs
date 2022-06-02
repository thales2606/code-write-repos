using Code.Writer.Repos.Domain.Entities;

namespace Code.Writer.Repos.Domain.Query
{
    public class ProjectResponse
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IEnumerable<Repository>? Repositories { get; set; }
    }
}
