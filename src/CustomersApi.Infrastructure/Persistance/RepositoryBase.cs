using CustomersApi.Domain;
using System.Collections.Generic;
using System.Linq;

namespace CustomersApi.Infrastructure.Persistance
{
    public abstract class RepositoryBase<T> where T: IEntity
    {
        protected List<T> _items;
        protected int _currentId;
        
        protected virtual RepoResult<T> GetItemById(int id)
        {
            var item = _items.SingleOrDefault(o => o.Id == id);

            if (item == null)
            {
                return new RepoResult<T>(item)
                {
                    Type = RepoResultType.NotFound
                };
            }
            return new RepoResult<T>(item)
            {
                Type = RepoResultType.Success
            };
        }

        protected virtual RepoResult<List<T>> GetItemsByIds(List<int> ids)
        {
            var items = _items.Where(i => ids.Any(id => i.Id == id)).ToList();

            if (items.Count == 0)
            {
                return new RepoResult<List<T>>(items)
                {
                    Type = RepoResultType.NotFound
                };
            }
            return new RepoResult<List<T>>(items)
            {
                Type = RepoResultType.Success
            };
        }

        protected virtual RepoResult<T> InsertItem(T item)
        {
            item.Id = _currentId;
            _items.Add(item);
            _currentId++;
            return new RepoResult<T>(item)
            {
                Type = RepoResultType.Success
            };
        }

        protected virtual RepoResult<T> UpdateItem(T item)
        {
            var repoItem = _items.SingleOrDefault(o => o.Id == item.Id);

            if (repoItem == null)
            {
                return new RepoResult<T>(item)
                {
                    Type = RepoResultType.NotFound
                };
            }
            _items.Remove(repoItem);
            _items.Add(item);
            return new RepoResult<T>(item)
            {
                Type = RepoResultType.Success
            };            
        }

        protected virtual RepoResult<T> DeleteItem(int id)
        {
            var item = _items.FirstOrDefault(o => o.Id == id);
            if (item == null)
            {
                return new RepoResult<T>(item)
                {
                    Type = RepoResultType.NotFound
                };
            }
            _items.Remove(item);

            return new RepoResult<T>(item)
            {
                Type = RepoResultType.Success
            };
        }
    }
}
