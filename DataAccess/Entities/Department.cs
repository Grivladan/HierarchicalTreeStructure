using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class Department : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
