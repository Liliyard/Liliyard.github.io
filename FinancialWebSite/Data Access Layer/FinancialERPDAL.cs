using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FinancialWebSite.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FinancialWebSite.Data_Access_Layer
{
    public class FinancialERPDAL : DbContext
    {
        public FinancialERPDAL() : base("FinancialERPDAL") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Classes> TClasses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Contention> Contentions { get; set; }
        public DbSet<Apply> Applys { get; set; }
        public DbSet<Diary> Diaries { get; set; }
        public DbSet<Operate> Operates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}