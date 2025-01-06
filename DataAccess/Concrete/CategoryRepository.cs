using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        public async Task<int> GetCategoryIdByName(string Name)
        {
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Categories.Where(x => x.Name == Name).Select(y => y.id).FirstOrDefaultAsync();
            }
        }
    }
}
