using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HierarchicalTree.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IEnumerable<T> Find(Func<T, bool> where,
           params Expression<Func<T, object>>[] navigationProperties);

        IQueryable<T> Query { get; }
    }
}
