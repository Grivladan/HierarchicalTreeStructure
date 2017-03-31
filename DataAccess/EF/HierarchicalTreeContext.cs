using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF
{
    public class HierarchicalTreeContext : DbContext
    {
        public HierarchicalTreeContext(DbContextOptions<HierarchicalTreeContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
