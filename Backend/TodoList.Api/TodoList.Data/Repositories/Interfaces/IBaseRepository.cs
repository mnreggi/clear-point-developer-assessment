using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoList.Data.DTO;

namespace TodoList.Data.Repositories.Interfaces
{
    public interface IBaseRepository<TModel> where TModel : class, IDataModel
    {
        /// <summary>
        /// Finds the Model that matched the Id passed async.
        /// This method won't track the entity
        /// </summary>
        /// <param name="id">Guid Id</param>
        /// <returns>Model found</returns>
        Task<TModel> FindByIdNoTrackingAsync(Guid id);
        
        /// <summary>
        /// Finds the Model that matched the Id passed async.
        /// This method will track the entity
        /// </summary>
        /// <param name="id">Guid Id</param>
        /// <returns>Model found</returns>
        Task<TModel> FindByIdTrackingAsync(Guid id);
        
        /// <summary>
        /// Verify if at least 1 model matched the condition passed on the predicate async.
        /// </summary>
        /// <param name="predicate">Condition to be matched</param>
        /// <returns>True if we found a match.</returns>
        Task<bool> AnyAsync(Expression<Func<TModel, bool>> predicate);
        
        /// <summary>
        /// Get all Model async.
        /// </summary>
        /// <returns></returns>
        Task<List<TModel>> GetAllAsync();

        /// <summary>
        /// Creates and Saves the entity. Throws an exception if something went wrong.
        /// </summary>
        /// <param name="item">Entity to be saved.</param>
        /// <returns>Task</returns>
        Task CreateAndSaveChangesAsync(TModel item);
        
        /// <summary>
        /// Updates and Saves the entity. Throws an exception if something went wrong.
        /// </summary>
        /// <param name="item">Entity to be updated.</param>
        /// <returns>Task</returns>
        Task UpdateAndSaveChangesAsync(TModel item);
    }
}