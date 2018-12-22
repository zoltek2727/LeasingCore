using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("Leasings")]
    public class Leasing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeasingId { get; set; }
        [Required]
        public DateTime LeasingStart { get; set; }
        [Required]
        public DateTime LeasingEnd { get; set; }
        [Required]
        public bool LeasingExtend { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<Report> Reports { get; set; }
        public ICollection<LeasingDetail> LeasingDetails { get; set; }
    }
}
