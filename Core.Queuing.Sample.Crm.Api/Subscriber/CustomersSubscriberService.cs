using Core.Queuing.Sample.Crm.Api.DAL;
using Core.Queuing.Sample.Crm.Api.Entity;
using Core.Queuing.Sample.Crm.Api.Model;
using DotNetCore.CAP;

namespace Core.Queuing.Sample.Crm.Api.Subscriber
{
    [CapSubscribe("Customers")]
    public class CustomersSubscriberService : ICapSubscribe
    {
        private readonly ServiceDbContext _dbContext;
        public CustomersSubscriberService(ServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [CapSubscribe("Create", isPartial: true)]
        public void Create(OrderEventCreated customer)
        {
            _dbContext.Customers.Add(new Customer
            {
                FirstName = customer.CustomerName,
                LastName = string.Empty,
                MobilNumber = customer.CustomerPhoneNumber
            });
            _dbContext.SaveChanges();

            return;
        }

        [CapSubscribe("Update", isPartial: true)]
        public void Update(string customer)
        {
            Console.WriteLine("Customer Updated");
            return;
        }


        [CapSubscribe("SendEmail", isPartial: true)]
        public void SendEmail(OrderEventCreated customer)
        {
            Console.WriteLine("Email Updated");

        }




    }


}
