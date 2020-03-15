using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PerreraNueva.Models;

namespace PerreraNueva.Services.Repository
{
    public class PerrosRepository: GenericRepository<Perros>, IPerrosRepository
    {
        private PerreraEntities _context;
        private DbSet<Perros> _table;

        public PerrosRepository(PerreraEntities context)
        {
            _context = context;
            _table = _context.Set<Perros>();
        }

        public PerrosRepository()
        {
            _context = new PerreraEntities();
            _table = _context.Set<Perros>();
        }

        public async Task<IEnumerable<Perros>> GetAll()
        {
            return await _table
                .Include(a => a.Jaulas)
                .Include(a => a.Razas)
                .ToListAsync().ConfigureAwait(false);
        }
    }
}