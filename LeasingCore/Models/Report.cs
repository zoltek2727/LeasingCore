using System;
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
        [Required]
        public DateTime ReportAdded { get; set; }

        public int LeasingDetailId { get; set; }
        [ForeignKey("LeasingDetailId")]
        public LeasingDetail LeasingDetail { get; set; }

        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
    }
}
