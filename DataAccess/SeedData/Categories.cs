using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace DataAccess.SeedData
{
    public class Categories : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.HasData(
                new Category { id = 1, Name = "Personal"},
                new Category { id = 2, Name = "Travel" },
                new Category { id = 3, Name = "Lifestyle" },
                new Category { id = 4, Name = "News" },
                new Category { id = 5, Name = "Marketting" },
                new Category { id = 6, Name = "Sports" },
                new Category { id = 7, Name = "Movies" }

            );
        }
    }
}
