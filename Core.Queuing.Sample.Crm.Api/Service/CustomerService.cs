using Core.Queuing.Sample.Crm.Api.DAL;
using Core.Queuing.Sample.Crm.Api.Entity;
using DotNetCore.CAP;
using System.Text.Json;

namespace Core.Queuing.Sample.Crm.Api.Service
{
    public interface ICustomerService
    {
        Task<bool> AddCustomer(CustomerInsert customerInsert);
        Task<bool> UpdateCustomer(Customer customerInsert);
    }

    public class CustomerService : ICustomerService
    {
        private readonly ServiceDbContext _dbContext;
        private readonly ICapPublisher _capPublisher;

        public CustomerService(ServiceDbContext dbContext, ICapPublisher capPublisher)
        {
            _dbContext = dbContext;
            _capPublisher = capPublisher;
        }

        public async Task<bool> AddCustomer(CustomerInsert customerInsert)
        {
            var @next = new Dictionary<string, string>();
            next.Add("next", "Orders.SendEmail,Orders.Create");
            next.Add("queue", "c");

            Customer customer = new()
            {
                FirstName = customerInsert.FirstName,
                LastName = customerInsert.LastName,
                MobilNumber = customerInsert.MobilNumber,
            };

            await _dbContext.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            await _capPublisher
                .PublishAsync("Customers.Create", contentObj: customer, @next, default);
            return true;
        }

        public async Task<bool> UpdateCustomer(Customer customerInsert)
        {
            Customer customer = new()
            {
                FirstName = customerInsert.FirstName,
                LastName = customerInsert.LastName,
                MobilNumber = customerInsert.MobilNumber,
                Id = customerInsert.Id
            };

            _dbContext.Update(customer);
            await _dbContext.SaveChangesAsync();
            var content = JsonSerializer.Serialize(customer);
            await _capPublisher.PublishAsync("Customers.Update", content);
            return true;
        }
    }
}
