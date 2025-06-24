using DinDin.Infra.Postgres.Models;
using Microsoft.EntityFrameworkCore;

namespace DinDin.Infra.Postgres
{
    public class DinDinDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<MonthlySummaryModel> MonthlySummaries { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DinDinDbContext).Assembly);
        }
    }
}