using System.ComponentModel.DataAnnotations.Schema;

namespace DonaMenina.Entities
{
    public class MergeClass
    {
        [ForeignKey("Sale")]
        public int SaleId { get; set; }

        [ForeignKey("Worker")]
        public int WorkerID { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int QuantitySold { get; set; }

        public Sale Sale { get; set; }
        public Worker Worker { get; set; }
        public Product Product { get; set; }
    }
}
