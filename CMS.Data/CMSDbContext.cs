using CMS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Data
{
    public class CMSDbContext:DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Note> Note { get; set; }
        public CMSDbContext(DbContextOptions<CMSDbContext> options)
            : base(options)
        {
        }
    }

    class CreateDatabase : IDesignTimeDbContextFactory<CMSDbContext>
    {
        public CMSDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CMSDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CMSDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new CMSDbContext(optionsBuilder.Options);
        }
    }
}
