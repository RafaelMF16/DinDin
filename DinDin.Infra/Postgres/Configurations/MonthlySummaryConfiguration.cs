using DinDin.Domain.MonthlySummaries;
using DinDin.Infra.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DinDin.Infra.Postgres.Configurations
{
    public class MonthlySummaryConfiguration : IEntityTypeConfiguration<MonthlySummaryModel>
    {
        public void Configure(EntityTypeBuilder<MonthlySummaryModel> builder)
        {
            builder.ToTable("MonthlySummaries");

            builder.HasKey(monthlySummary => monthlySummary.Id);

            builder.Property(monthlySummary => monthlySummary.Month)
                .IsRequired();

            builder.Property(monthlySummary => monthlySummary.Year)
                .IsRequired();

            builder.Property(monthlySummary => monthlySummary.TotalIncome)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(monthlySummary => monthlySummary.TotalExpense)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(monthlySummary => monthlySummary.Balance)
                .HasColumnType("decimal(18,2)")
            .IsRequired();
        }
    }
}