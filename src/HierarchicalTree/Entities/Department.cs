using HierarchicalTree.Interfaces;

namespace HierarchicalTree.Entities
{
    public class Department : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Offering Offering { get; set; }
        public int OfferingId { get; set; }
    }
}
