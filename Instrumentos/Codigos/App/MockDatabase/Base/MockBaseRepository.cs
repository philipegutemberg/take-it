using System.Collections.Generic;
using System.Linq;

namespace MockDatabase.Base
{
    internal abstract class MockBaseRepository<TEntity>
    {
#pragma warning disable SA1401
        protected readonly IDictionary<string, TEntity> Storage;
#pragma warning restore SA1401

        protected MockBaseRepository()
        {
            Storage = new Dictionary<string, TEntity>();
        }

        protected void Insert(string key, TEntity entity)
        {
            Storage.Add(key, entity);
        }

        protected void InsertOrUpdate(string key, TEntity entity)
        {
            if (Storage.ContainsKey(key))
            {
                Storage[key] = entity;
            }
            else
            {
                Insert(key, entity);
            }
        }

        protected TEntity? GetByKey(string key)
        {
            if (Storage.ContainsKey(key))
                return Storage[key];

            return default;
        }

        protected IEnumerable<TEntity> GetAllEntities()
        {
            return Storage.Select(s => s.Value);
        }
    }
}