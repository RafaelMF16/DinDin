using DinDin.Infra.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DinDin.Infra.Postgres.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenModel>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenModel> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(refreshToken => refreshToken.Id);

            builder.Property(refreshToken => refreshToken.TokenHash)
                .IsRequired();

            builder.Property(refreshToken => refreshToken.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(refreshToken => refreshToken.ExpiresAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(refreshToken => refreshToken.Revoked)
                .IsRequired();

            builder.HasOne<UserModel>()
                .WithMany()
                .HasForeignKey(refreshToken => refreshToken.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}