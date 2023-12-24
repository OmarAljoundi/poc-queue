using Core.Queuing.Sample.Store.Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace Core.Queuing.Sample.Store.Api.DAL
{
    public class ServiceDbContext : DbContext
    {
        public DbSet<Order> Order { get; set; }
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
