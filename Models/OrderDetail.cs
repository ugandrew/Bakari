using System.ComponentModel.DataAnnotations;

namespace Bakari.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int? CustomerId { get; set; }
        [DisplayFormat(DataFormatString = "{0: #,#}", ApplyFormatInEditMode = true)]
        public decimal Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0: #,#}", ApplyFormatInEditMode = true)]
        public decimal UnitPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0: #,#}", ApplyFormatInEditMode = true)]
        public decimal TotalPrice { get; set; }

        public string? Orderby { get; set; }
        public required Order Order { get; set; }
        public Customer? Customer { get; set; }
        public required Item Item { get; set; }
    }
}
