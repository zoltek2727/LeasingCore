using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("Companys")]
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "CompanyId must be 50 characters or less"), MinLength(2)]
        public string CompanyName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "CompanyHeadquarters must be 50 characters or less"), MinLength(2)]
        public string CompanyHeadquarters { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "CompanyNIP must be 10 numbers or less"), MinLength(2)]
        [RegularExpression(@"^\d$")]
        public string CompanyNIP { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
