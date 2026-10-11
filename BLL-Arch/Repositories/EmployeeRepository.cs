using BLL_Arch.Interfaces;
using DAL_Arch.Contexts;
using DAL_Arch.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Arch.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly DALDbContext _dbContext;

        public EmployeeRepository(DALDbContext dbContext):base(dbContext)
        {
           _dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmployeeByName(string SearchValue)
        => _dbContext.Employees.Include(E => E.Department).Where(E=>E.Name.ToLower().Contains(SearchValue.ToLower()));

        public IQueryable<Employee> GetEmployeeOfAddress(string address)
        => _dbContext.Employees.Where(E => E.Address == address);
        
    }
}
