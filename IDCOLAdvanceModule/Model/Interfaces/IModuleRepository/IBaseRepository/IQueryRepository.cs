using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IBaseRepository
{
    public interface IQueryRepository<T> where T:class
    {
        ICollection<T> GetAll(params Expression<Func<T, object>>[] includes);
        ICollection<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T GetFirstOrDefaultBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    }
}
