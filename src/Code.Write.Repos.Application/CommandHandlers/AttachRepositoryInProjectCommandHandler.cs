using AutoMapper;
using Code.Writer.Repos.Domain.Commands;
using Code.Writer.Repos.Domain.Contracts.Infrastructure.DataProviders.Repositories;
using Code.Writer.Repos.Domain.Entities;
using MediatR;

namespace Code.Writer.Repos.Application.CommandHandlers
{
    public class AttachRepositoryInProjectCommandHandler : IRequestHandler<AttachRepositoryInProjectCommand>
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        public AttachRepositoryInProjectCommandHandler(IMapper mapper,
            IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }
        public async Task<Unit> Handle(AttachRepositoryInProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.FindByIdAsync(request.ProjectId);
            var repositories = new List<Repository>();
            if (project.Repositories != null)
                repositories.AddRange(project.Repositories);
            repositories.Add(_mapper.Map<Repository>(request));
            project.Repositories = repositories;
            await _projectRepository.ReplaceOneAsync(project);
            return Unit.Value;
        }
    }
}
