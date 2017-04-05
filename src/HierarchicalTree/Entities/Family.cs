using HierarchicalTree.Interfaces;

namespace HierarchicalTree.Entities
{
    public class Family : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Business Business { get; set; }
        public int BusinessId { get; set; }
    }
}
