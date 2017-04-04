using HierarchicalTree.Interfaces;
using HierarchicalTree.Data;

namespace HierarchicalTree.Repository
{
    public sealed class RepositoryFactory : IRepositoryFactory
    {
        IRepository<T> IRepositoryFactory.CreateRepository<T>(ApplicationDbContext context)
        {
            return new Repository<T>(context);
        }
    }
}
