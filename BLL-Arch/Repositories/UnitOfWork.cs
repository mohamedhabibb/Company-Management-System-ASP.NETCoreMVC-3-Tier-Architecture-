using BLL_Arch.Interfaces;
using DAL_Arch.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Arch.Repositories
{
    public class UnitOfWork : IUnitOfWork ,IDisposable
    {
        private readonly DALDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        public UnitOfWork(DALDbContext dbContext)
        {
            EmployeeRepository = new EmployeeRepository(dbContext);
            DepartmentRepository = new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> CompleteAsync()
        {
           return await _dbContext.SaveChangesAsync();   
            
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
