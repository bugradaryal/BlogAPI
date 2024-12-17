using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Entities.DTO_s
{
    public class TokenValidationViewModel
    {
        public User user {  get; set; }
        public string role { get; set; }
    }
}
