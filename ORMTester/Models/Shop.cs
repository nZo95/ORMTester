using System.ComponentModel.DataAnnotations;

namespace ORMTester.Models
{
    public class Shop
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        
        public ICollection<Product>? Products { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
