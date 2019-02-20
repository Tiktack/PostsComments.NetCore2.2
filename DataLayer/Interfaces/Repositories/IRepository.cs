using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task Insert(TEntity item);
        Task Delete(int id);
        Task Update(TEntity item);
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
    }
}
