using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBanking.Infra.Database.EF.Entities;

namespace SimpleBanking.Infra.Database.EF.Configurations;

public class MerchantConfiguration : IEntityTypeConfiguration<EFMerchant>
{
    public void Configure(EntityTypeBuilder<EFMerchant> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Id).HasMaxLength(40);

        builder.Property(x => x.ResponsableFullName).HasMaxLength(128).IsRequired();

        builder.Property(x => x.HashedPassword).HasMaxLength(128).IsRequired();
        builder.Property(x => x.Salt).HasMaxLength(128).IsRequired();

        builder.Property(x => x.CNPJ).HasMaxLength(14).IsRequired();
        builder.HasIndex(x => x.CNPJ).IsUnique();

        builder.Property(x => x.EmailAddress).HasMaxLength(255).IsRequired();
        builder.HasIndex(x => x.EmailAddress).IsUnique();

        builder.Property(x => x.Debit).IsRequired();

        builder.ToTable(t => t.HasCheckConstraint("CK_MERCHANT_Balance", @"""Debit"" >= 0"));
    }
}


