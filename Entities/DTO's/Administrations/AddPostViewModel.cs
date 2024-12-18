using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s.Administrations
{
    public class AddPostViewModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(64)]
        public string Title { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(720)]
        public string Content { get; set; }
        public byte Image { get; set; }
    }
}
