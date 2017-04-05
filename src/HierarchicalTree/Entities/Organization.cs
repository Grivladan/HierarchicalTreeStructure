using HierarchicalTree.Interfaces;
using HierarchicalTree.Models;

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
    }
}
