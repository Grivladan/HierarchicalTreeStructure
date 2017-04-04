using System.Collections.Generic;
using System.Linq;

namespace HierarchicalTree.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Create(T item);
        void Update(T item);
        void Delete(int id);

        IQueryable<T> Query { get; }
    }
}
