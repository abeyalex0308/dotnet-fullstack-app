using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Preqin.Core.Entities;

namespace Preqin.Infrastructure.Configurations
{
    public class InvestorConfiguration : IEntityTypeConfiguration<Investor>
    {
        public void Configure(EntityTypeBuilder<Investor> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasIndex(i => i.Id).IsUnique();
            builder.Property(i => i.Name).IsRequired().HasMaxLength(100);
            builder.Property(i => i.InvestorType).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Country).IsRequired().HasMaxLength(50);
            builder.Property(i => i.DateAdded).IsRequired();
            builder.Property(i => i.LastUpdated).IsRequired();

            
            builder.HasMany(i => i.Commitments)
                   .WithOne(c => c.Investor)
                   .HasForeignKey(c => c.InvestorId); 
        }
    }
}
