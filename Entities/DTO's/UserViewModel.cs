using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class UserViewModel
    {
        [Required]
        public string Id { get; set; }
        [MinLength(4)]
        [MaxLength(32)]
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [MinLength(3)]
        [MaxLength(32)]
        [Required]
        public string Name { get; set; }
        [MinLength(3)]
        [MaxLength(32)]
        [Required]
        public string Surname { get; set; }
        [StringLength(10, ErrorMessage = "Invalid phone number!")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Wrong character type!")]
        public string PhoneNumber { get; set; }
        [MaxLength(160)]
        public string Address { get; set; }

    }
}
