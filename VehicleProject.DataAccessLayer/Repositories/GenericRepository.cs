using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VehicleProject.CoreLayer.Entities.Abstract;
using VehicleProject.CoreLayer.Repositories;
using VehicleProject.DataAccessLayer.Context;

namespace VehicleProject.DataAccessLayer.Repositories
{
    public class GenericRepository<Tentity> : IGenericRepository<Tentity> 
        where Tentity : class, IBaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<Tentity> _dbSet;

        public GenericRepository(VehicleProjectContext context)
        {
            _context = context;
            _dbSet = context.Set<Tentity>();
        }

        public async Task AddAsync(Tentity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<Tentity>> GetAllAsync()
        {
            return await _dbSet.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<Tentity> GetByIdAsync(int id)
        {   
            var entity = await _dbSet.FindAsync(id);
            //if (entity != null)
            //{
            //    _context.Entry(entity).State = EntityState.Detached;
            //}
            return entity;
        }

        public void HardRemove(Tentity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Remove(Tentity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.UtcNow;
            Update(entity);
        }

        public Tentity Update(Tentity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<Tentity> Where(Expression<Func<Tentity, bool>> predicate)
        {
            return _dbSet.Where(x => x.IsDeleted == false).Where(predicate);
        }
    }
}
