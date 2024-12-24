using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }

        public bool AccountSuspended { get; set; }
        public ICollection<Comment> comments { get; set; }
        public ICollection<Like> likes { get; set; }
    }
}
