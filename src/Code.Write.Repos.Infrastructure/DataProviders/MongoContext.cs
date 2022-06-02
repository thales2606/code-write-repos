using Code.Writer.Repos.Domain.Contracts.Infrastructure.DataProviders;
using Code.Writer.Repos.Domain.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Code.Writer.Repos.Infrastructure.DataProviders
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; }
        public MongoClient MongoClient { get; private set; }
        public IClientSessionHandle Session { get; private set; }
        private readonly List<Func<Task>> _commands;

        public MongoContext(IOptions<AppSettings> options)
        {
            _commands = new List<Func<Task>>();

            RegisterConventions();
            MongoClient = new MongoClient(options.Value.MongoDbConnection);
            Database = MongoClient.GetDatabase(options.Value.MongoDbName);

        }
        private void RegisterConventions()
        {
            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(true)
            };
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
        }
        public async Task<int> SaveChanges()
        {
            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }
        public Task AddCommand(Func<Task> func)
        {
            return Task.Run(() => _commands.Add(func));
        }
        public void Dispose()
        {
            while (Session != null && Session.IsInTransaction)
                Thread.Sleep(TimeSpan.FromMilliseconds(100));

            GC.SuppressFinalize(this);
        }
    }
}
