using MediatR;

namespace Code.Writer.Repos.Domain.Commands
{
    public class CreateProjectCommand : IRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
