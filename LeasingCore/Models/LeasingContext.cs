using Microsoft.EntityFrameworkCore;

namespace LeasingCore.Models
{
    public class LeasingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=LeasingDB;Trusted_Connection=True;");
        }
    }
}
