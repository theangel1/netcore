using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<bool> Create(T entity);
        Task<IList<T>> FindAll();
        Task<bool> Save();
    }
}