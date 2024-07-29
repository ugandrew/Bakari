using System.ComponentModel.DataAnnotations;

namespace Bakari.Models
{
    public class Basket
    {
        public int BasketId { get; set; }

        public int ItemId { get; set; }

        [DisplayFormat(DataFormatString = "{0: #,#}", ApplyFormatInEditMode = true)]
        public decimal Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0: #,#}", ApplyFormatInEditMode = true)]
        public decimal UnitPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0: #,#}", ApplyFormatInEditMode = true)]
        public decimal TotalPrice { get; set; }

        public string? ImagePath { get; set; }


        public required Item Item { get; set; }
    }
}
