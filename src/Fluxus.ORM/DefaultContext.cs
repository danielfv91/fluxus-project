using Fluxus.Domain.Entities;
using Fluxus.ORM.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Fluxus.ORM
{
    public class DefaultContext : DbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<DailyConsolidation> DailyConsolidations => Set<DailyConsolidation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
