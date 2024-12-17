using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Comment
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public int post_id { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }

        public User users { get; set; }
        public Post posts { get; set; }
    }
}
