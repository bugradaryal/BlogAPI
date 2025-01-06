using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Post
    {
        public int id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }

        public byte[] Image { get; set; }

        public int category_id { get; set; }
       
        
        
        public Category categories { get; set; }
        public ICollection<Comment> comments { get; set; }
        public ICollection<Like> likes { get; set; }
    }
}
