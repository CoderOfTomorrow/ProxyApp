using Domain.Common;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Persistence.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sync
{
    public class SyncRepository<T> : IRepository<T> where T : Entity
    {
        private readonly DatabaseOptions config;
        public SyncRepository(IOptions<DatabaseOptions> config)
        {
            this.config = config.Value;
        }
        public Task<bool> AddEntity(T student)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntities(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntity(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetEntities()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetEntity(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpsertEntity(T entity)
        {
            foreach (var obj in config.SlaveDatabases)
            {
                var db = new MongoClient(config.ConnectionString).GetDatabase(obj);
                var data = db.GetCollection<T>(obj);

                await data.ReplaceOneAsync(i => i.Id == entity.Id, entity, new ReplaceOptions() { IsUpsert = true });
            }

            return true;
        }
    }
}
