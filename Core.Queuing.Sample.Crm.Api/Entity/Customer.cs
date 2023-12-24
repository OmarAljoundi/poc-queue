using System.ComponentModel.DataAnnotations;

namespace Core.Queuing.Sample.Crm.Api.Entity
{
    public class Customer : CustomerInsert
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
    public class CustomerInsert
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilNumber { get; set; }
    }
}
