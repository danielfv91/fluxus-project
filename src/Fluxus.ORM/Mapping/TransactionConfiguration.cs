using Fluxus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fluxus.ORM.Mappings
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Amount)
                .IsRequired();

            builder.Property(t => t.Type)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Date)
                .IsRequired();

            builder.Property(t => t.UserId)
                .IsRequired();
        }
    }
}
