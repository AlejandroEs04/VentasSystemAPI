using Microsoft.EntityFrameworkCore;
using VentasSystemAPI.Data;

namespace VentasSystemAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApiDbContext _context;
        private readonly DbSet<T> _table;

        public GenericRepository(ApiDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> Get(int id)
        {
            var entity = await _table.FindAsync(id);
            return entity ?? throw new Exception($"{typeof(T).Name} no existe");
        }

        public async Task<T> Add(T entity)
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _table.FindAsync(id);
            if (entity == null) throw new Exception($"{typeof(T).Name} no existe");

            _table.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
