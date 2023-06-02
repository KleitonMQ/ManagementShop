using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DonaMenina.Entities
{
    public class Worker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkerId { get; set; }
        public string Name { get; set; }
        public bool IsAdm { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
