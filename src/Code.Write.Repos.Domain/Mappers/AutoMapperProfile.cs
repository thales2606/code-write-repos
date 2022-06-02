using AutoMapper;
using Code.Writer.Repos.Domain.Commands;
using Code.Writer.Repos.Domain.Entities;
using Code.Writer.Repos.Domain.Query;

namespace Code.Writer.Repos.Domain.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateProjectCommand, Project>();

            CreateMap<Project, ProjectResponse>()
                .ForMember(x => x.Id, member => member.MapFrom(source => source.Id.ToString()));

            CreateMap<AttachRepositoryInProjectCommand, Repository>();
        }
    }
}
