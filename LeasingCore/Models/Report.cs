using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("Reports")]
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportId { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "ReportDescription must be 250 characters or less"), MinLength(2)]
        public string ReportDescription { get; set; }

        public int LeasingId { get; set; }
        [ForeignKey("LeasingId")]
        public Leasing Leasing { get; set; }

        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
    }
}
