using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(80, ErrorMessage = "ProductName must be 80 characters and no less than 10"), MinLength(10)]
        public string ProductName { get; set; }
        [Required]
        public decimal ProductPrice { get; set; }
        [Required]
        public int ProductAvailability { get; set; }
        [Required]
        public string ProductCode { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public ICollection<LeasingDetail> LeasingDetails { get; set; }
        public ICollection<ProductParam> ProductParams { get; set; }
    }
}
