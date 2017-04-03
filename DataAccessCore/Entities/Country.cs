using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class Country : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
