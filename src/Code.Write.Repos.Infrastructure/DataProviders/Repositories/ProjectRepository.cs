using Code.Writer.Repos.Domain.Contracts.Infrastructure.DataProviders.Repositories;
using Code.Writer.Repos.Domain.Entities;
using Code.Writer.Repos.Domain.Settings;
using Microsoft.Extensions.Options;

namespace Code.Writer.Repos.Infrastructure.DataProviders.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IOptions<AppSettings> options) : base(options)
        {
        }
    }
}
