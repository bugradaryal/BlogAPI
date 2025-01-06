using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Bussiness.Abstract
{
    public interface ICategoryServices
    {
        Task<Category> GetCategoryIdByName(string Name);
    }
}
