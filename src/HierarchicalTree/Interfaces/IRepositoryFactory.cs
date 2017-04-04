using HierarchicalTree.Data;

namespace HierarchicalTree.Interfaces
{
    public interface IRepositoryFactory
    {
        IRepository<T> CreateRepository<T>(ApplicationDbContext context) where T : class, IEntity;
    }
}
