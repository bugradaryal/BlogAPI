﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s.Administrations
{
    public class UpdatePostViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(250)]
        public string Title { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(1800)]
        public string Content { get; set; }
        public byte[] Image { get; set; }
    }
}
