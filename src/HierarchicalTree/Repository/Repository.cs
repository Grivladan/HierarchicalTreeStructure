using System;
using System.Collections.Generic;
using System.Linq;
using HierarchicalTree.Interfaces;
using Microsoft.EntityFrameworkCore;
using HierarchicalTree.Data;

namespace HierarchicalTree.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _entities;
        public Repository(ApplicationDbContext context)
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
