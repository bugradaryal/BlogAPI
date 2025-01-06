using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities;

namespace Bussiness.Concrete
{
    public class CategoryManager : ICategoryServices
    {
        private ICategoryRepository _categoryRepository;
        public CategoryManager() 
        {
            _categoryRepository = new CategoryRepository();
        }

        public async Task<Category> GetCategoryIdByName(string Name)
        {
            return await _categoryRepository.GetCategoryIdByName(Name);
        }
    }
}
