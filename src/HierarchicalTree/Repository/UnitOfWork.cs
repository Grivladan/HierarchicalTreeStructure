using System;
using HierarchicalTree.Entities;
using HierarchicalTree.Interfaces;
using Microsoft.EntityFrameworkCore;
using HierarchicalTree.Data;

namespace HierarchicalTree.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private bool _isDisposed;
        private readonly IRepositoryFactory _repositoryFactory;

        public UnitOfWork(ApplicationDbContext context, IRepositoryFactory repositoryFactory)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
        }

        private IRepository<Business> _businessRepository;
        private IRepository<Country> _countriesRepository;
        private IRepository<Department> _departmentsRepository;
        private IRepository<Family> _familiesRepository;
        private IRepository<Offering> _offeringsRepository;
        private IRepository<Organization> _organizationsRepository;
        private IRepository<User> _usersRepository;

        public IRepository<Business> Businesses
        {
            get
            {
                return _businessRepository ?? (_businessRepository = _repositoryFactory.CreateRepository<Business>(_context));
            }
        }

        public IRepository<Country> Countries
        {
            get
            {
                return _countriesRepository ?? (_countriesRepository = _repositoryFactory.CreateRepository<Country>(_context));
            }
        }

        public IRepository<Department> Departments
        {
            get
            {
                return _departmentsRepository ?? (_departmentsRepository = _repositoryFactory.CreateRepository<Department>(_context));
            }
        }

        public IRepository<Family> Families
        {
            get
            {
                return _familiesRepository ?? (_familiesRepository = _repositoryFactory.CreateRepository<Family>(_context));
            }
        }

        public IRepository<Offering> Offerings
        {
            get
            {
                return _offeringsRepository ?? (_offeringsRepository = _repositoryFactory.CreateRepository<Offering>(_context));
            }
        }

        public IRepository<Organization> Organizations
        {
            get
            {
                return _organizationsRepository ?? (_organizationsRepository = _repositoryFactory.CreateRepository<Organization>(_context));
            }
        }

        public IRepository<User> Users
        {
            get
            {
                return _usersRepository ?? (_usersRepository = _repositoryFactory.CreateRepository<User>(_context));
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _isDisposed = true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
