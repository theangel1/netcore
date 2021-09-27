using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Persistence.DapperConnection;

namespace Application.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IList<T>> FindAll();
        Task<T> FindById(int id);
        Task<bool> IsExists(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();
        Task<PaginacionModel> GetPaginacion( PaginacionCursoRequestDTO request);             

    }
}