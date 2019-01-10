using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("Assortments")]
    public class Assortment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssortmentId { get; set; }
        [Required]
        [MaxLength(80, ErrorMessage = "AssortmentName must be 80 characters or less"), MinLength(2)]
        public string AssortmentName { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "AssortmentBrand must be 30 characters or less"), MinLength(2)]
        public string AssortmentBrand { get; set; }
        [Required]
        public decimal AssortmentPrice { get; set; }

        public int ParamId { get; set; }
        [ForeignKey("ParamId")]
        public Param Param { get; set; }

        public ICollection<ProductAssortment> ProductAssortments { get; set; }
    }
}
