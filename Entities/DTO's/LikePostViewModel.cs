using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class LikePostViewModel
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
