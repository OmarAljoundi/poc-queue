namespace Core.Queuing.Sample.Crm.Api.Model
{
    public class OrderEventCreated
    {
        public string CustomerPhoneNumber { get; set; }
        public string CustomerName { get; set; }
        public string Quantity { get; set; }
        public string Status { get; set; }
    }
}
