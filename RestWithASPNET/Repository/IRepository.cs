using System.Collections.Generic;

namespace RestWithASPNET.Repository
{
    public interface IRepository<T>
    {
        Task<T> CreateAsync(T item);
        Task<T> FindByIdAsync(long id);
        Task<List<T>> FindAllAsync();
        Task<T> UpdateAsync(T item);
        Task DeleteAsync(long id); 
    }
}