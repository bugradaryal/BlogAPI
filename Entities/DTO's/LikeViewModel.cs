using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class LikeViewModel
    {
        [Required]
        public int userId { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool isLiked { get; set; }
    }
}
