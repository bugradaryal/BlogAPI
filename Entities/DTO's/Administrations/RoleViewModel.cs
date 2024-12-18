using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s.Administrations
{
    public class RoleViewModel
    {
        [Required]
        public string userId { get; set; }
        [Required]
        [AllowedValues("User", "Administrator")]
        public string Role { get; set ; }
    }
}
