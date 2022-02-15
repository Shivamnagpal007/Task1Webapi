using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Models;

namespace taskWebapi.Data
{
    public class ApplicationDbcontext:DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options):base(options)
        {

        }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
        public DbSet<User> users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EmployeeDepartment>().HasKey(table => new {
                table.empId,
                table.depId
            });
        }


    }
}
