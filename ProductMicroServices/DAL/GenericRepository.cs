using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProductMicroServices.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    { 
        private ProductDBContext _context;
        private DbSet<T> table;
        private bool _disposed;

        public GenericRepository(ProductDBContext _context)
        {
            this._context = _context;
            this.table = _context.Set<T>();
        }
        /// <summary>
        /// Get a collection of all entites
        /// </summary>
        /// <returns>A return of all entities</returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }

        /// <summary>
        /// Gets an entity by ID
        /// </summary>
        /// <param name="id"> The ID of entity to retrive</param>
        /// <returns>The entity object if found, otherwise null</returns>
        public async Task<T> GetById(int id)
        {
            #pragma warning disable CS8603 // Possible null reference return.
            return await table.FindAsync(id);
            #pragma warning restore CS8603 // Possible null reference return.
        }

        /// <summary>
        /// Add an entity
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns></returns>
        public async Task Insert(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Modify the entity, If exist
        /// </summary>
        /// <param name="entity">The entity to modify</param>
        /// <returns></returns>
        public async Task Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            //return Task.CompletedTask;
        }

        /// <summary>
        /// Delete the entity by id 
        /// </summary>
        /// <param name="id">The Id of entity to delete</param>
        /// <returns></returns>
        public async Task Delete(int id)
        { 
            T? existing = table.Find(id);
            if(existing != null)
            {
                table.Remove(existing);
                await _context.SaveChangesAsync();
            }
           
        }

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.DisposeAsync();
        //            // Dispose other managed resources.
        //        }
        //        //release unmanaged resources.
        //    }
        //    _disposed = true;
        //}

    }
}

