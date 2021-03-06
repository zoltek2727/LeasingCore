﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeasingCore.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Leasing> Leasings { get; set; }
        public DbSet<LeasingDetail> LeasingDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductAssortment> ProductParams { get; set; }
        public DbSet<Param> Params { get; set; }
        public DbSet<Assortment> Assortments { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhotoProduct> PhotoProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=LeasingDB;Trusted_Connection=True;");
        }
    }
}