using HierarchicalTree.Interfaces;
using System.Collections.Generic;

namespace HierarchicalTree.Entities
{
    public class Country : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Organization Organization { get; set; }
        public int OrganizationId { get; set; }
        public ICollection<Business> Businesses { get; set; } 

        public Country()
        {
            Businesses = new List<Business>();
        }
    }
}
