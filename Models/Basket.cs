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
        public virtual ICollection<Basket>? Baskets { get; set; }
        [DisplayFormat(DataFormatString = "{0: #,#}", ApplyFormatInEditMode = true)]
        public decimal? TotalBasket
        {


            get
            {
                if (Baskets == null) { return 0; }
                return Baskets.Sum(x => x.TotalPrice);
            }

        }
    }
}
