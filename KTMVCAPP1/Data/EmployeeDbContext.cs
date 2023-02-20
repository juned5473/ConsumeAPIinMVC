using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KTMVCAPP1.Models;

namespace KTMVCAPP1.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext()
        {
        }

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<SignIn> LogIn { get; set; }
        public DbSet<KTMVCAPP1.Models.EmployeeDTO> EmployeeDTO { get; set; }


    }
}
