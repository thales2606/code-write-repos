using MongoDB.Driver;

namespace Code.Writer.Repos.Domain.Contracts.Infrastructure.DataProviders
{
    public interface IMongoContext : IDisposable
    {
        Task AddCommand(Func<Task> func);
        Task<int> SaveChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
