using System;
using System.Threading.Tasks;

namespace ProductMicroServices.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}

