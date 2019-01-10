using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("Params")]
    public class Param
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParamId { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "ParamName must be 30 characters or less"), MinLength(2)]
        public string ParamName { get; set; }

        public ICollection<Assortment> Assortments { get; set; }
    }
}
