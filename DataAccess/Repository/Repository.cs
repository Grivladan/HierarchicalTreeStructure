using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;

namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        public IQueryable<T> Query
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Create(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(T item)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            return Query.FirstOrDefault(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Query.ToList();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
