using System.ComponentModel.DataAnnotations;

namespace ORMTester.Models
{
    public class ProductType
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
