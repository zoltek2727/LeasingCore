using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeasingCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50, ErrorMessage = "User Firstname must be 50 characters or less"), MinLength(2)]
        public string UserFirstName { get; set; }
        [MaxLength(50, ErrorMessage = "User Surname must be 50 characters or less"), MinLength(2)]
        public string UserSurname { get; set; }

        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public ICollection<Leasing> Leasings { get; set; }
    }
}
