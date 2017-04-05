using HierarchicalTree.Interfaces;
using HierarchicalTree.Models;
using System.Collections.Generic;

namespace HierarchicalTree.Entities
{
    public enum OrganizationType
    {
        GeneralPartnership,
        LimitedPartnership,
        LimitedLiabilityCompany,
        IncorporatedCompany,
        SocialEnterprise
    }

    public class Organization : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OrganizationType? Type { get; set; }
        public ApplicationUser Owner { get; set; }
        public string OwnerId { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
    }
}
