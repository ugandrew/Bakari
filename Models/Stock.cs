using System.ComponentModel.DataAnnotations;

namespace Bakari.Models
{
    public class Stock
    {
        public int StockId { get; set; }

        public int ItemId { get; set; }
        public required string ItemName { get; set; }

        public required string Category { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#}", ApplyFormatInEditMode = true)]
        public decimal Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#}", ApplyFormatInEditMode = true)]
        public decimal SalePrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#}", ApplyFormatInEditMode = true)]
        public decimal CostPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#}", ApplyFormatInEditMode = true)]
        public decimal Total { get; set; }

        public string? ImagePath { get; set; }

        public required Item Item { get; set; }
        public required DateTime LastUpDated { get; set; }
       
    }
}
