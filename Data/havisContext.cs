using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using havis2._0.Models;

namespace havis2._0.Models
{
    public class havisContext : DbContext
    {
        public havisContext(DbContextOptions<havisContext> options): base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(@"server=DESKTOP-EAOHLEB\SQLEXPRESS;Database=havisDB; User Id=sa; Password=BACKPACK;Trusted_Connection=True;");
        }
        public DbSet<Nurse> nurse { get; set; }
        public DbSet<AccMan> accMan { get; set; }
        public DbSet<Security> security { get; set; }
        public DbSet<Student> student { get; set; }
        public DbSet<Teacher> teacher { get; set; }
        public DbSet<Parent> parent { get; set; }
        public DbSet<Report> report { get; set; }
        public DbSet<Class> class1 { get; set; }
    }
}
