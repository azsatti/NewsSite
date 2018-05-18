using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.DataAccess
{
    public interface IDataRepository<TEntity, U> where TEntity : class
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> Get(U id);
        Task Add(TEntity b);
        Task<bool> Update(U id, TEntity b);
        Task<bool> Delete(U id);
    }
}
