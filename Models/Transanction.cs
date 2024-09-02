using System.ComponentModel.DataAnnotations;

namespace Bakari.Models
{
    public enum TransanctionType
    {
        CashIn,
        CashOut
    }
    public class Transanction
    {
        public int Id { get; set; }

        
        public TransanctionType Type { get; set; } // CashIn or CashOut

        [DisplayFormat(DataFormatString = "{0:#,#}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }

        public required string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.UtcNow;
       
        public virtual ICollection<Transanction>? Transanctions { get; set; }
        public string? PerformedBy { get; set; }
        public decimal? Balance
        {


            get
            {
                if (Transanctions == null) { return 0; }
                return Transanctions.Sum(t => t.Type == TransanctionType.CashOut ? t.Amount : -t.Amount);
            }

        }

    }
}
