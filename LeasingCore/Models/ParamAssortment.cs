using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("ParamAssortments")]
    public class ParamAssortment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParamAssortmentId { get; set; }
        [Required]
        [MaxLength(80, ErrorMessage = "ParamAssortmentName must be 80 characters or less"), MinLength(2)]
        public string ParamAssortmentName { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "ParamAssortmentBrand must be 30 characters or less"), MinLength(2)]
        public string ParamAssortmentBrand { get; set; }

        public int ParamId { get; set; }
        [ForeignKey("ParamId")]
        public Param Param { get; set; }
    }
}
