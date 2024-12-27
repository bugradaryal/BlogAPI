using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class CommentViewModel
    {
        [Required]
        public string user_id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public int post_id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(360)]
        public string Content { get; set; }
    }
}
