using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class RegisterViewModel
    {
        [MinLength(3)]
        [MaxLength(32)]
        [Required]
        public string Name { get; set; }
        [MinLength(3)]
        [MaxLength(32)]
        [Required]
        public string Surname { get; set; }
        [MinLength(4)]
        [MaxLength(32)]
        [Required]
        public string UserName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [MinLength(6)]
        [MaxLength(16)]
        [Required]
        public string Password { get; set; }
        [MaxLength(160)]
        public string Adress { get; set; }
    }
}
