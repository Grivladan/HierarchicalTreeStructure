using HierarchicalTree.Interfaces;
using System.Collections.Generic;

namespace HierarchicalTree.Entities
{
    public class Offering : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Family Family { get; set; }
        public int FamilyId { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
