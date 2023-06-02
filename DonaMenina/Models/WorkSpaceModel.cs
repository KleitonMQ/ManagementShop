using DonaMenina.Entities;

namespace DonaMenina.Models
{
    public class WorkSpaceModel
    {
        public WorkSpaceModel()
        {
            ShoppingCart = new List<Product>();
            Products= new List<Product>();

            IdProducts = new List<int>();
            QuantitySold= new List<int>();

            Sale = new Sale();
        }
        public Worker Worker { get; set; }
        public List<Product> Products { get; set; }
        public List<Product> ShoppingCart { get; set; }
        public Sale Sale { get; set; }
        public List<int> IdProducts { get; set; }
        public List<int> QuantitySold { get; set; }

    }
}
