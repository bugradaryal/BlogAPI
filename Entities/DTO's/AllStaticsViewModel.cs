using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class AllStaticsViewModel
    {
        public ICollection<PostStaticsViewModel> PostStatics { get; set; }
        public ICollection<CommentStaticsViewModel> CommentStatics { get; set; }
        public ICollection<LikeStaticsViewModel> LikeStatics { get; set; }
        public int UserCount { get; set; }
    }
}
