using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("LeasingDetails")]
    public class LeasingDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeasingDetailId { get; set; }
        [Required]
        public int LeasingDetailAmount { get; set; }
        [Required]
        public bool LeasingDetailExtend { get; set; }
        [Required]
        public DateTime LeasingDetailEnd { get; set; }

        public int LeasingId { get; set; }
        [ForeignKey("LeasingId")]
        public Leasing Leasing { get; set; }

        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public ICollection<Report> Reports { get; set; }
    }
}
