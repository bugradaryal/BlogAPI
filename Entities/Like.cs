using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Like
    {
        public int id { get; set; }
        public int post_id { get; set; }
        public DateTime Date { get; set; }
        public string user_id { get; set; }

        public Post posts { get; set; }
        public User users { get; set; }

    }
}
