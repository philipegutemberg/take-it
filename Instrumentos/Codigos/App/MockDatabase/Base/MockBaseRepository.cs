using System.Collections.Generic;
using System.Linq;

namespace MockDatabase.Base
{
    internal abstract class MockBaseRepository<TEntity>
    {
        protected IDictionary<string, TEntity> _storage;

        public MockBaseRepository()
        {
            _storage = new Dictionary<string, TEntity>();
        }

        protected void Insert(string key, TEntity entity)
        {
            _storage.Add(key, entity);
        }

        protected TEntity? GetByKey(string key)
        {
            if (_storage.ContainsKey(key))
                return _storage[key];

            return default;
        }

        protected IEnumerable<TEntity> GetAllEntities()
        {
            return _storage.Select(s => s.Value);
        }
    }
}