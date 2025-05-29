using DinDin.Infra.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DinDin.Infra.Postgres.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(user => user.Email)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(user => user.Password)
                   .IsRequired();

            builder.HasIndex(user => user.Email)
                   .IsUnique();
        }
    }
}