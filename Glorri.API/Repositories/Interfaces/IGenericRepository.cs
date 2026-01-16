using Glorri.API.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace Glorri.API.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public DbSet<T> Table { get; }
        Task<bool> AddAsync(T entity);
        bool Remove(T entity);
        bool Toggle(T entity); //1 defe basanda silir, 1 defe basanda berpa edir.
        bool Update(T entity);
        IQueryable<T> GetAll(bool isTracking, params string[] includes);
        Task<T> GetByIdAsync(int id, bool isTracking, params string[] includes);
        Task<int> SaveAsync();
    }
}
