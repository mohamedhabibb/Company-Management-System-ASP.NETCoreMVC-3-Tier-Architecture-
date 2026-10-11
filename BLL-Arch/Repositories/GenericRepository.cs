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
    public class GenericRepository<T> : IGenericRepository<T> where T : class 
    {
        private readonly DALDbContext _dbcontext;
        public GenericRepository(DALDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task AddAsync(T item)
        {
           await _dbcontext.AddAsync(item);
        }

        public void Delete(T item)
        {
            _dbcontext.Remove(item);
            //return _dbcontext.SaveChanges();

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)_dbcontext.Employees.Include(E=>E.Department).ToList();
            }

            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        => await _dbcontext.Set<T>().FindAsync(id);
        

        public void Update(T item)
        {
            _dbcontext.Update(item);
            //return _dbcontext.SaveChanges();
        }
    }
}
