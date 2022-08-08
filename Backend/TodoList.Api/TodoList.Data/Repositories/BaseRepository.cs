using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoList.Data.Contexts;
using TodoList.Data.DTO;
using TodoList.Data.Repositories.Interfaces;

namespace TodoList.Data.Repositories
{
    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class, IDataModel
    {
        private readonly DbContext _context;

        public BaseRepository(TodoContext context)
        {
            _context = context;
        }
        
        /// <inheritdoc />
        public Task<TModel> FindByIdNoTrackingAsync(Guid id)
            => _context.Set<TModel>().AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);

        /// <inheritdoc />
        public Task<TModel> FindByIdTrackingAsync(Guid id)
            => _context.Set<TModel>().AsTracking().FirstOrDefaultAsync(item => item.Id == id);

        /// <inheritdoc />
        public virtual Task<bool> AnyAsync(Expression<Func<TModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<List<TModel>> GetAllAsync()
            => _context.Set<TModel>().AsNoTracking().ToListAsync();

        /// <inheritdoc />
        public Task CreateAndSaveChangesAsync(TModel item)
        {
            Create(item);
            return SaveChangesAsync();
        }

        /// <inheritdoc />
        public Task UpdateAndSaveChangesAsync(TModel item)
        {
            _context.Set<TModel>().Update(item);
            return SaveChangesAsync();
        }

        private void Create(TModel item)
        {
            _context.Set<TModel>().Add(item);
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}