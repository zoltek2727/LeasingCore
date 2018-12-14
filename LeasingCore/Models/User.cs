using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UserId { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "UserName must be 30 characters or less"), MinLength(2)]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [MaxLength(20, ErrorMessage = "User Password must be 20 characters or minimum 6"), MinLength(6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,20}$")]
        public string UserPassword { get; set; }
        [MaxLength(50, ErrorMessage = "User Firstname must be 50 characters or less"), MinLength(2)]
        public string UserFirstName { get; set; }
        [MaxLength(50, ErrorMessage = "User Surname must be 50 characters or less"), MinLength(2)]
        public string UserSurname { get; set; }
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company {get; set;}

        public ICollection<Leasing> Leasings { get; set; }
    }
}
