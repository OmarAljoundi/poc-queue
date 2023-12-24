using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Queuing.Sample.Store.Api.Entity
{
    public class Order : OrderInsert
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
    public class OrderInsert
    {
        [NotMapped]
        public string CustomerName { get; set; }

        [NotMapped]
        public string CustomerPhoneNumber { get; set; }

        public Guid CustomerId { get; set; }
        public string Quantity { get; set; }
        public string Status { get; set; }
    }
}
