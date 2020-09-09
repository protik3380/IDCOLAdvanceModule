using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IDCOLAdvanceModule.Model.Interfaces.Repository.BaseRepository
{
    public interface IRepository<T> where T:class
    {
        bool Insert(T entity);
        bool Insert(ICollection<T> entityCollection);
        bool Edit(T entity);
        bool Delete(T entity);
        T GetFirstOrDefaultBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        ICollection<T> GetAll(params Expression<Func<T, object>>[] includes);
        ICollection<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    }
}
