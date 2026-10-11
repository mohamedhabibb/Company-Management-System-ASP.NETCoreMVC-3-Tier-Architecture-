using BLL_Arch.Interfaces;
using DAL_Arch.Contexts;
using DAL_Arch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Arch.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DALDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
