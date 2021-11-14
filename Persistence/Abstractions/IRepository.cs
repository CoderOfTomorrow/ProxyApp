using Domain.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Abstractions
{
    public interface IRepository<T> where T : Entity
    {
        public Task<List<T>> GetEntities();
        public Task<T> GetEntity(Guid id);
        public Task<bool> AddEntity(T student);
        public Task<bool> DeleteEntity(Guid id);
        public Task<bool> DeleteEntities(List<Guid> ids);

    }
}
