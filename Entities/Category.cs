using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Category
    {
        public int id { get; set; }
        public string Name { get; set; }

        public Post post { get; set; }
    }
}
