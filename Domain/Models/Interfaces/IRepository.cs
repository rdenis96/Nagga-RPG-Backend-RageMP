using System.Collections.Generic;

namespace Domain.Models.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T entity);
        T Update(T entity);
        IEnumerable<T> GetAll();
        bool Delete(T entity);
        T GetById(int id);
    }
}
