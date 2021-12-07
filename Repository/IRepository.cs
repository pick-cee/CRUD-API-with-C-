using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace havis2._0.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    }
}
