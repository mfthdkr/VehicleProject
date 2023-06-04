using Microsoft.EntityFrameworkCore;
using VehicleProject.CoreLayer.UnitOfWork;
using VehicleProject.DataAccessLayer.Context;

namespace VehicleProject.DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(VehicleProjectContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommmitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
