using Core.Queuing.Sample.Crm.Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace Core.Queuing.Sample.Crm.Api.DAL
{
    public class ServiceDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
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
