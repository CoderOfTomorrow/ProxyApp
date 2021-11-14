using Domain.Common;
using MongoDB.Driver;
using Persistence.Abstractions;
using Persistence.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Persistence.Implimentations
{
    public class MongoDbRepository<T> : IRepository<T> where T : Entity
    {
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<T> data;
        public MongoDbRepository(MongoDbConfiguration mongoDbConfig)
        {

            db = mongoDbConfig.GetDatabase() ?? throw new ArgumentNullException(nameof(mongoDbConfig));
            data = db.GetCollection<T>(mongoDbConfig.TableName);
        }
        public async Task<bool> AddEntity(T entity)
        {
            await data.InsertOneAsync(entity);

            return true;
        }

        public async Task<bool> UpsertEntity(T entity)
        {
            await data.ReplaceOneAsync(i => i.Id == entity.Id, entity, new ReplaceOptions() { IsUpsert = true });

            return true;
        }

        public async Task<bool> DeleteEntity(Guid id)
        {
            if (await data.FindAsync(i => i.Id == id) != null)
            {
                await data.DeleteOneAsync(i => i.Id == id);
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteEntities(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                if (!await DeleteEntity(id))
                    return false;
            }

            return true;
        }

        public async Task<T> GetEntity(Guid id)
        {
            var entities = await data.AsQueryable().ToListAsync();
            return entities.FirstOrDefault(e => e.Id == id);
        }

        public async Task<List<T>> GetEntities()
        {
            return await data.AsQueryable().ToListAsync();
        }
    }
}
