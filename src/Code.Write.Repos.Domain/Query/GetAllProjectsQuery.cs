using MediatR;

namespace Code.Writer.Repos.Domain.Query
{
    public class GetAllProjectsQuery : IRequest<IEnumerable<ProjectResponse>>
    {
    }
}
