using DataLayer.EntityContexts;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataLayer.Common
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        public abstract void Create(T entity);

        public abstract IEnumerable<T> GetAll();

        public abstract T GetById(int id);

        public bool Delete(T entity)
        {
            bool changesSaved = false;
            using (var context = new MysqlContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                changesSaved = context.SaveChanges() > 0;
            }

            return changesSaved;
        }

        public T Update(T entity)
        {
            bool changesSaved = false;
            using (var context = new MysqlContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                changesSaved = context.SaveChanges() > 0;
            }
            return changesSaved ? entity : default(T);
        }
    }
}