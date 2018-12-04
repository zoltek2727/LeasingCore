using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "RoleName must be 20 characters or less"), MinLength(2)]
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
