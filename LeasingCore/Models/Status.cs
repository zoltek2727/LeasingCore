using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("Statuses")]
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "StatusName must be 30 characters or less"), MinLength(2)]
        public string StatusName { get; set; }

        public ICollection<Report> Reports { get; set; }
    }
}
