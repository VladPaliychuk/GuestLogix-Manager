using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBoard.BLL.Interfaces.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(string id);
        Task AddAsync(TEntity entity); 
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(string id);
    }
}
