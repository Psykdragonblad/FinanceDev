using FinanceDev.Domain;
using Microsoft.EntityFrameworkCore;
namespace FinanceDev.Infraestructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<DI1> DI1 { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=finance.db");
        }
    }
}
