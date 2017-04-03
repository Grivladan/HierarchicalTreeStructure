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
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Offering> Offerings { get; set; }
        public DbSet<Organization> Organizations { get; set; }
    }
}
