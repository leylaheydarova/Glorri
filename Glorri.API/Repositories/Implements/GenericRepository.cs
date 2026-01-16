using Glorri.API.Contexts;
using Glorri.API.Models.BaseModels;
using Glorri.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Glorri.API.Repositories.Implements
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();


        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry entry = await _context.AddAsync(entity);
            return entry.State == EntityState.Added;
        }

        public IQueryable<T> GetAll(bool isTracking, params string[] includes)
        {
            var query = Table.AsQueryable();
            if (!isTracking) query = query.AsNoTracking();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }

        public async Task<T> GetByIdAsync(int id, bool isTracking, params string[] includes)
        {
            var query = Table.AsQueryable();
            if (!isTracking) query = query.AsNoTracking();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            T entity = await query.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public bool Remove(T entity)
        {
            EntityEntry entry = _context.Remove(entity);
            return entry.State == EntityState.Deleted;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool Toggle(T entity)
        {
            entity.IsDeleted = !entity.IsDeleted;
            _context.Update(entity);
            return entity.IsDeleted;
        }

        public bool Update(T entity)
        {
            EntityEntry entry = _context.Update(entity);
            return entry.State == EntityState.Modified;
        }
    }
}
