using DataAccess.Interfaces;
using SimpleInjector;

namespace DataAccess.Repository
{
    public sealed class RepositoryFactory : IRepositoryFactory
    {
        private readonly Container container;

        public RepositoryFactory(Container container)
        {
            this.container = container;
        }

        IRepository<T> IRepositoryFactory.CreateRepository<T>()
        {
            return this.container.GetInstance<Repository<T>>();
        }
    }
}
