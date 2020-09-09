using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IBaseRepository;

namespace IDCOLAdvanceModule.Repository.Base
{
    public class BaseQueryRepository<T> : IQueryRepository<T> where T : class
    {
        protected DbContext db { get; set; }

        public BaseQueryRepository(DbContext db)
        {
            this.db = db;
        }

        public DbSet<T> Table
        {
            get { return db.Set<T>(); }
        }


        public T GetFirstOrDefaultBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return includes
                .Aggregate(
                    Table.AsNoTracking().AsQueryable(),
                    (current, include) => current.Include(include),
                    c => c.FirstOrDefault(predicate)
                );
        }

        public ICollection<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            return includes
                .Aggregate(
                   Table.AsNoTracking().AsQueryable(),
                    (current, include) => current.Include(include)
                ).ToList();
        }

        public ICollection<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return includes
               .Aggregate(
                   Table.AsNoTracking().AsQueryable(),
                   (current, include) => current.Include(include),
                  c => c.Where(predicate)
               ).ToList();
        }
    }
}
