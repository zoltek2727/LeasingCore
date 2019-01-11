using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("ProductAssortments")]
    public class ProductAssortment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductAssortmentId { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int AssortmentId { get; set; }
        [ForeignKey("AssortmentId")]
        public Assortment Assortment { get; set; }
    }
}
