using Core.Queuing.Abstractions;
using Core.Queuing.Model;
using Core.Queuing.Sample.Store.Api.DAL;
using Core.Queuing.Sample.Store.Api.Entity;
using Core.Queuing.Sample.Store.Api.Model;
using DotNetCore.CAP;

namespace Core.Queuing.Sample.Store.Api.Service
{
    public interface IOrderService
    {
        Task<bool> AddOrder(OrderInsert customerInsert);
    }

    public class OrderService : IOrderService
    {
        private readonly ServiceDbContext _dbContext;
        private readonly IPublisher _publisher;

        public OrderService(ServiceDbContext dbContext, IPublisher publisher)
        {
            _dbContext = dbContext;
            _publisher = publisher;
        }

        public async Task<bool> AddOrder(OrderInsert order)
        {
            List<SequentialEvents> NextEvents = new();



            await _publisher
                .PublishAsync(new OrderEventCreated
                {
                    CustomerPhoneNumber = order.CustomerPhoneNumber,
                    CustomerName = order.CustomerName,
                    Quantity = order.Quantity,
                    Status = order.Status,
                }, NextEvents, default);


            return true;
        }
    }
}
