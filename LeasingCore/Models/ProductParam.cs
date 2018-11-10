using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("ProductParams")]
    public class ProductParam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductParamId { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int ParamId { get; set; }
        [ForeignKey("ParamId")]
        public Param Param { get; set; }
    }
}
