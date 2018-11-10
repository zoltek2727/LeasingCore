using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("LeasingDetailParams")]
    public class LeasingDetailParam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeasingDetailParamId { get; set; }

        public int LeasingDetailId { get; set; }
        [ForeignKey("LeasingDetailId")]
        public LeasingDetail LeasingDetail { get; set; }

        public int? ParamId { get; set; }
        [ForeignKey("ParamId")]
        public Param Param { get; set; }

        public int ParamAssortmentId { get; set; }
        [ForeignKey("ParamAssortmentId")]
        public ParamAssortment ParamAssortment { get; set; }
    }
}
