﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.SeedData
{
    public class UserRoles : IEntityTypeConfiguration<IdentityRole>
    {
       public void Configure(EntityTypeBuilder<IdentityRole> entity)
        {
            entity.HasData(
                new IdentityRole { Name = "Administrator", NormalizedName = "Administrator".ToUpper() },
                new IdentityRole { Name = "User", NormalizedName = "User".ToUpper() }
            );
        }
    }
}