using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DonaMenina.Entities
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleId { get; set; }
        public DateTime DataSale { get; set; }
        public string PaymentMethod { get; set; }
        public int PriceDiscount { get; set; }
        public decimal TotalSale { get; set; }
    }
}