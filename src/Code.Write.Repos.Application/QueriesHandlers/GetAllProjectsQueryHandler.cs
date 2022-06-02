using AutoMapper;
using Code.Writer.Repos.Domain.Contracts.Infrastructure.DataProviders.Repositories;
using Code.Writer.Repos.Domain.Query;
using MediatR;

namespace Code.Writer.Repos.Application.QueriesHandlers
{
    internal class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public GetAllProjectsQueryHandler(IProjectRepository projectRepository,
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProjectResponse>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectResponse>>(result);
        }
    }
}
