using AutoMapper;
using Code.Writer.Repos.Domain.Commands;
using Code.Writer.Repos.Domain.Contracts.Infrastructure.DataProviders.Repositories;
using Code.Writer.Repos.Domain.Entities;
using MediatR;

namespace Code.Writer.Repos.Application.CommandHandlers
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        public CreateProjectCommandHandler(IMapper mapper,
            IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }
        public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Project>(request);
            await _projectRepository.InsertOneAsync(entity);
            return Unit.Value;
        }
    }
}
