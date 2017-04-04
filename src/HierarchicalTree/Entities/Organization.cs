using HierarchicalTree.Interfaces;

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
    }
}
