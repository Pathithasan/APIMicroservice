using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProductMicroServices.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IDisposable
    { 
        private ProductDBContext _context = null;
        private DbSet<T> table = null;

        public GenericRepository(ProductDBContext _context)
        {
            this._context = _context;
            this.table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        { 
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            _context.Set<T>().AddAsync(obj);
            Save();

        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}

