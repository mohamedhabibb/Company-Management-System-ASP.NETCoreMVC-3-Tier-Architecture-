using DAL_Arch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Arch.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IQueryable<Employee> GetEmployeeOfAddress(string address);

        IQueryable<Employee> GetEmployeeByName(string SearchValue);
    }
}
