using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class EmailVeificationViewModel
    {
        [Required]
        public string userId { get; set; }
        [Required]
        public string emailConfUrl { get; set; }
    }
}
