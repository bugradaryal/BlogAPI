﻿using System;
using DataAccess.SeedData;
using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DataDbContext : IdentityDbContext<User>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress; Database=BlogAPI; Trusted_Connection=True; MultipleActiveResultSets=true; TrustServerCertificate=True;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().Property(x => x.Name).HasColumnType("nvarchar").IsRequired().HasMaxLength(32);
            modelBuilder.Entity<User>().Property(x => x.Surname).HasColumnType("nvarchar").IsRequired().HasMaxLength(32);
            modelBuilder.Entity<User>().Property(x => x.Address).HasColumnType("nvarchar").HasMaxLength(160).HasDefaultValue("Undefined");
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<User>().Property(x => x.AccountSuspended).HasColumnType("bit ").HasDefaultValue(false);

            modelBuilder.Entity<Category>().HasKey(x => x.id);
            modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnType("nvarchar").IsRequired().HasMaxLength(64);

            modelBuilder.ApplyConfiguration(new SeedData.UserRoles());
            modelBuilder.ApplyConfiguration(new SeedData.Categories());

            modelBuilder.Entity<Post>().HasKey(x => x.id);
            modelBuilder.Entity<Post>().Property(x => x.Title).HasColumnType("nvarchar").IsRequired().HasMaxLength(250);
            modelBuilder.Entity<Post>().Property(x => x.Date).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Post>().Property(x => x.Content).HasColumnType("nvarchar").HasMaxLength(1800).IsRequired();
            modelBuilder.Entity<Post>().Property(x => x.Image).HasColumnType("varbinary(max)");
            modelBuilder.Entity<Post>().Property(x => x.category_id).IsRequired();

            modelBuilder.Entity<Like>().HasKey(x => x.id);
            modelBuilder.Entity<Like>().Property(x => x.post_id).IsRequired();
            modelBuilder.Entity<Like>().Property(x => x.Date).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Like>().Property(x => x.user_id).HasColumnType("nvarchar(450)");
            
            modelBuilder.Entity<Comment>().HasKey(x => x.id);
            modelBuilder.Entity<Comment>().Property(x => x.user_id).HasColumnType("nvarchar(450)");
            modelBuilder.Entity<Comment>().Property(x => x.post_id).IsRequired();
            modelBuilder.Entity<Comment>().Property(x => x.Date).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Comment>().Property(x => x.Content).HasColumnType("nvarchar").HasMaxLength(720);
            modelBuilder.Entity<Comment>().Property(x => x.UserName).HasColumnType("nvarchar").HasMaxLength(240).IsRequired();

            modelBuilder.Entity<Comment>().HasOne<User>(x => x.users).WithMany(y => y.comments).HasForeignKey(x => x.user_id);
            modelBuilder.Entity<Comment>().HasOne<Post>(x => x.posts).WithMany(y => y.comments).HasForeignKey(x => x.post_id);

            modelBuilder.Entity<Like>().HasOne<Post>(x => x.posts).WithMany(y => y.likes).HasForeignKey(x => x.post_id);
            modelBuilder.Entity<Like>().HasOne<User>(x => x.users).WithMany(y => y.likes).HasForeignKey(x => x.user_id);

            modelBuilder.Entity<Post>().HasOne<Category>(x => x.categories).WithMany(y => y.post).HasForeignKey(x => x.category_id);
        }
    }
}
