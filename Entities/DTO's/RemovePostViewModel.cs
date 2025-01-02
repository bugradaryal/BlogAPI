using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class RemovePostViewModel
    {
        [Required]
        public int postId { get; set; }
    }
}
