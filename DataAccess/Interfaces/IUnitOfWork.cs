using DataAccess.Entities;
using System;

namespace DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Business> Businesses { get; }
        IRepository<Country> Countries { get; }
        IRepository<Department> Departments { get; }
        IRepository<Family> Families { get; }
        IRepository<Offering> Offerings { get; }
        IRepository<Organization> Organizations { get; }
        IRepository<User> Users { get; }

        void Save();
    }
}
