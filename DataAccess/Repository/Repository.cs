using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DbContext _context;
        private DbSet<T> _entities;
        public Repository(DbContext context)
        {
            _context = context;
        }

        protected DbSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }

        public IQueryable<T> Query
        {
            get { return Entities; }
        }

        public void Create(T item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");
                Entities.Add(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var entity = GetById(id);
                if(entity == null)
                {
                    throw new ArgumentNullException("id");
                }
                Entities.Remove(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetById(int id)
        {
            return Query.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return Query.ToList();
        }

        public void Update(T item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                _context.Entry(item).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
