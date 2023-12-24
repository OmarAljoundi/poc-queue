using Core.Queuing.Sample.Store.Api.DAL;
using Core.Queuing.Sample.Store.Api.Entity;
using Core.Queuing.Sample.Store.Api.Model;
using DotNetCore.CAP;


namespace Core.Queuing.Sample.Store.Api.Subscriber
{
    [CapSubscribe("Orders")]
    public class OrderSubscriberService : ICapSubscribe
    {
        private readonly ServiceDbContext _dbContext;
        public OrderSubscriberService(ServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [CapSubscribe("Create", isPartial: true)]
        public void Create(OrderEventCreated order)
        {
            using (_dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Order.Add(new Order
                    {
                        CustomerId = order.CustomerId,
                        Quantity = order.Quantity,
                        Status = order.Status,
                    });
                    _dbContext.SaveChanges();
                    _dbContext.Database.CommitTransaction();
                }
                catch (Exception ex)
                {
                    _dbContext.Database.RollbackTransaction();
                }
            }
        }

        [CapSubscribe("Cancel", isPartial: true)]
        public void Cancel(OrderEventCreated order)
        {
            Console.WriteLine("Heool World");
        }


        [CapSubscribe("SendEmail", isPartial: true)]
        public void SendEmail()
        {
            EmailService.SendEmail("omaraljoundi@gmail.com", "Order Created!", "Congrats your order has been created");
        }
    }
}
