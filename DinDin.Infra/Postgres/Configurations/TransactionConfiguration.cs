using DinDin.Infra.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DinDin.Infra.Postgres.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<TransactionModel>
    {
        public void Configure(EntityTypeBuilder<TransactionModel> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(transaction => transaction.Id);

            builder.Property(transaction => transaction.Type)
                .IsRequired();

            builder.Property(transaction => transaction.IncomeCategory);

            builder.Property(transaction => transaction.ExpenseCategory);

            builder.Property(transaction => transaction.Amont)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(transaction => transaction.Description)
                .HasMaxLength(255);

            builder.Property(transaction => transaction.TransactionDate)
                .IsRequired()
                .HasColumnType("timestamp without time zone");

            builder.HasOne<MonthlySummaryModel>()
                .WithMany()
                .HasForeignKey(transaction => transaction.MonthlySummaryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
