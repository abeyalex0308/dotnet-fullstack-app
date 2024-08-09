using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Preqin.Core.Entities;

namespace Preqin.Infrastructure.Configurations
{
    public class CommitmentConfiguration : IEntityTypeConfiguration<Commitment>
    {
        public void Configure(EntityTypeBuilder<Commitment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.AssetClass).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Amount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(c => c.Currency).IsRequired().HasMaxLength(3);

 
            builder.HasOne(c => c.Investor)
                   .WithMany(i => i.Commitments)
                   .HasForeignKey(c => c.InvestorId);
            builder.HasIndex(c => c.InvestorId);
            builder.HasIndex(c => new { c.InvestorId, c.AssetClass });
        }
    }
}
