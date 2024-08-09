using Microsoft.EntityFrameworkCore;
using Preqin.Core.Entities;
using Preqin.Infrastructure.Configurations;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Preqin.Infrastructure.Data
{
    public class PreqinDbContext : DbContext
    {
        public PreqinDbContext(DbContextOptions<PreqinDbContext> options) : base(options) { }

        public DbSet<Investor> Investors { get; set; }
        public DbSet<Commitment> Commitments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CommitmentConfiguration());
            modelBuilder.ApplyConfiguration(new InvestorConfiguration());
        }
    }
}
