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

        public string Id { get; set; }
        [ForeignKey("Id")]
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<LeasingDetail> LeasingDetails { get; set; }
    }
}
