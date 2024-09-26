using Microsoft.EntityFrameworkCore;
using Protocol.Infrastructure.Data.Configurations;

namespace Protocol.Infrastructure.Data
{
    public class ProtocolDbContext : DbContext
    {
        public DbSet<Domain.Entities.Protocol> Protocols { get; set; }

        public ProtocolDbContext(DbContextOptions<ProtocolDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProtocolConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
