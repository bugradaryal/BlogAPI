using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(32)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
