using MediatR;

namespace Code.Writer.Repos.Domain.Commands
{
    public class AttachRepositoryInProjectCommand : IRequest
    {
        public string? ProjectId { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
    }
}
