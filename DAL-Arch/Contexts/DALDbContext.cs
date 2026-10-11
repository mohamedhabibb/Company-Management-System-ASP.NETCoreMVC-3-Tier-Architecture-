using DAL_Arch.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Arch.Contexts
{
    public class DALDbContext : IdentityDbContext<ApplicationUser>
    {
       public DALDbContext(DbContextOptions<DALDbContext> options):base(options)
        {

        } 
        public DbSet<Department> Departments { get; set; }  
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<IdentityUser> Users { get; set; } 
        //public DbSet<IdentityRole> Roles { get; set; }  
    }
}
