using System.ComponentModel.DataAnnotations;

namespace Bakari.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }
        public string? OrderNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#}", ApplyFormatInEditMode = true)]
        public decimal SubTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#}", ApplyFormatInEditMode = true)]
        public decimal Discount { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#}", ApplyFormatInEditMode = true)]
        public decimal OrderTotal { get; set; }

        public  string? Orderby { get; set; }    
    }
}
