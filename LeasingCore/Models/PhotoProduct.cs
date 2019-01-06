using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("PhotoProducts")]
    public class PhotoProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhotoProductId { get; set; }
        [Required]
        public bool PhotoProductIsDefault { get; set; }

        public int PhotoId { get; set; }
        [ForeignKey("PhotoId")]
        public Photo Photo { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
