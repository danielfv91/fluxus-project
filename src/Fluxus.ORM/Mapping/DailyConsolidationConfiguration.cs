using Fluxus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fluxus.ORM.Mapping
{
    public class DailyConsolidationConfiguration : IEntityTypeConfiguration<DailyConsolidation>
    {
        public void Configure(EntityTypeBuilder<DailyConsolidation> builder)
        {
            builder.ToTable("DailyConsolidations");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.Date).IsRequired();
            builder.Property(c => c.TotalCredits).HasPrecision(18, 2);
            builder.Property(c => c.TotalDebits).HasPrecision(18, 2);
        }
    }
}
