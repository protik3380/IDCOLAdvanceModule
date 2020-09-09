using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager
{
    public interface IManager<T> where T : class
    {
        bool Insert(T entity);
        bool Insert(ICollection<T> entityCollection);
        bool Edit(T entity);
        bool Delete(long id);
        T GetById(long id);

        ICollection<T> GetAll();
    }
}
