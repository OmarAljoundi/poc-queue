namespace Core.Queuing.Sample.Store.Api.Model
{
    public class OrderEventCreated
    {
        public string CustomerPhoneNumber { get; set; }
        public string CustomerName { get; set; }
        public string Quantity { get; set; }
        public string Status { get; set; }
        public Guid CustomerId { get; set; }
    }
}
