using FinanceDev.Domain;
using Microsoft.EntityFrameworkCore;
namespace FinanceDev.Infraestructure
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<DI1> DI1 { get; set; }
        public DbSet<MesVencimento> MesVencimento { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=finance.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações de modelo
            modelBuilder.Entity<MesVencimento>()
                .HasKey(a => a.Id);

            // Seed de dados
            modelBuilder.Entity<MesVencimento>().HasData(
                new MesVencimento { Id = 1, Codigo = "F", Mes = "Janeiro" },
                new MesVencimento { Id = 2, Codigo = "G", Mes = "Fevereiro" },
                new MesVencimento { Id = 3, Codigo = "H", Mes = "Março" },
                new MesVencimento { Id = 4, Codigo = "J", Mes = "Abril" },
                new MesVencimento { Id = 5, Codigo = "K", Mes = "Maio" },
                new MesVencimento { Id = 6, Codigo = "M", Mes = "Junho" },
                new MesVencimento { Id = 7, Codigo = "N", Mes = "Julho" },
                new MesVencimento { Id = 8, Codigo = "Q", Mes = "Agosto" },
                new MesVencimento { Id = 9, Codigo = "U", Mes = "Setembro" },
                new MesVencimento { Id = 10, Codigo = "V", Mes = "Outubro" },
                new MesVencimento { Id = 11, Codigo = "X", Mes = "Novembro" },
                new MesVencimento { Id = 12, Codigo = "Z", Mes = "Dezembro" }
            );
        }

    }
}
