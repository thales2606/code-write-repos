using Code.Writer.Repos.Domain.Contracts.Attributes;
using Code.Writer.Repos.Domain.Contracts.Domain.Entities;
using Code.Writer.Repos.Domain.Contracts.Infrastructure.DataProviders.Repositories;
using Code.Writer.Repos.Domain.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Code.Writer.Repos.Infrastructure.DataProviders.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
     where TEntity : IEntity
    {
        private readonly IMongoCollection<TEntity> _collection;
        public BaseRepository(IOptions<AppSettings> settings)
        {
            var database = new MongoClient(settings.Value.MongoDbConnection).GetDatabase(settings.Value.MongoDbName);
            _collection = database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            var attr = documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                .FirstOrDefault();
            if (attr != null)
            {
                var bsonCollectionAttribute = (BsonCollectionAttribute)attr;
                return bsonCollectionAttribute.CollectionName;
            }
            return "";
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public virtual IEnumerable<TEntity> FilterBy(
            Expression<Func<TEntity, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TEntity, bool>> filterExpression,
            Expression<Func<TEntity, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual TEntity FindById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, objectId);
            return _collection.Find(filter).SingleOrDefault();
        }

        public virtual Task<TEntity> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, objectId);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }


        public virtual void InsertOne(TEntity document)
        {
            _collection.InsertOne(document);
        }

        public virtual Task InsertOneAsync(TEntity document)
        {
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public void InsertMany(ICollection<TEntity> documents)
        {
            _collection.InsertMany(documents);
        }


        public virtual async Task InsertManyAsync(ICollection<TEntity> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public void ReplaceOne(TEntity document)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }

        public virtual async Task ReplaceOneAsync(TEntity document)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public void DeleteOne(Expression<Func<TEntity, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        public Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public void DeleteById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, objectId);
            _collection.FindOneAndDelete(filter);
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, objectId);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        public void DeleteMany(Expression<Func<TEntity, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }

        public Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _collection
                 .AsQueryable()
                 .ToListAsync();
        }
    }
}
