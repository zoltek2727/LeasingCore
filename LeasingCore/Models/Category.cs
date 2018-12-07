using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "CategoryName must be 30 characters or less"), MinLength(2)]
        public string CategoryName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
