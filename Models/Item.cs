using System.ComponentModel.DataAnnotations;

namespace Bakari.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        public int CategoryId { get; set; }
        public required string ItemCode { get; set; }
        public required string ItemName { get; set; }


        public string? Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,#}", ApplyFormatInEditMode = true)]
        public decimal ItemPrice { get; set; }

        public string? ImagePath { get; set; }


        public required Category Category { get; set; }
    }
}
