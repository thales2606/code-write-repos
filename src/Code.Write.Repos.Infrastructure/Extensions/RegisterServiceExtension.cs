using AutoMapper;
using Code.Writer.Repos.Domain.Contracts.Infrastructure.DataProviders;
using Code.Writer.Repos.Domain.Contracts.Infrastructure.DataProviders.Repositories;
using Code.Writer.Repos.Domain.Contracts.Infrastructure.GitHub;
using Code.Writer.Repos.Domain.Mappers;
using Code.Writer.Repos.Domain.Settings;
using Code.Writer.Repos.Infrastructure.DataProviders;
using Code.Writer.Repos.Infrastructure.DataProviders.Repositories;
using Code.Writer.Repos.Infrastructure.Github;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using Code.Writer.Repos.Domain.Commands;

namespace Code.Writer.Repos.Infrastructure.Extensions
{
    public static class RegisterServiceExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.Configure<GithubSettings>(configuration.GetSection("GithubSettings"));
            
            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IGithubService, GithubService>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());

            var assembly = AppDomain.CurrentDomain.Load("Code.Writer.Repos.Application");
            services.AddMediatR(assembly);

        }
    }
}
