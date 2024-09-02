using System.ComponentModel.DataAnnotations;

namespace Bakari.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public required string CustomerName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public required string PhoneNumber { get; set; }
    }
}
