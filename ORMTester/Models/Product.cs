using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORMTester.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ?Name { get; set; }
        [Required]
        public int? ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        public ProductType? ProductType { get; set; }

        public int? ShopId { get; set; }

        [ForeignKey("ShopId")]
        public Shop? Shop { get; set; }
    }
}
