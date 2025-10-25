using Microsoft.EntityFrameworkCore;
using RestaurantApp.Application.Interfaces;
using RestaurantApp.Domain.Models;
using RestaurantApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepo(RestaurantDbContext dbContext)
        {
           _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }


        public async Task CreateAsync(T entity)
        {
           await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
          _dbSet.Remove(entity);
        }

      

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public Task SaveAsync()
        {
          return _dbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
           _dbSet.Update(entity);
        }
    }
}
