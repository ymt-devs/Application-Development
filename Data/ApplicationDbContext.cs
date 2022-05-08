using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GroupCoursework.Models;

namespace GroupCoursework.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GroupCoursework.Models.Category> Category { get; set; }
        public DbSet<GroupCoursework.Models.Product> Product { get; set; }
        public DbSet<GroupCoursework.Models.ProductStock> ProductStock { get; set; }
        public DbSet<GroupCoursework.Models.Customer> Customer { get; set; }
        public DbSet<GroupCoursework.Models.Purchase> Purchase { get; set; }
        public DbSet<GroupCoursework.Models.PurchaseDetails> PurchaseDetails { get; set; }
        public DbSet<GroupCoursework.Models.Sales> Sales { get; set; }
        public DbSet<GroupCoursework.Models.SalesDetail> SalesDetail { get; set; }
    }
}
