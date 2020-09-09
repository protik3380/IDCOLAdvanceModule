using IDCOLAdvanceModule.Model.Interfaces.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace IDCOLAdvanceModule.Repository.Base
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DbContext db { get; set; }

        public BaseRepository(DbContext db)
        {
            this.db = db;
        }

        public DbSet<T> Table
        {
            get
            { 
                var set = db.Set<T>();
                return set;
            }
        }

        public bool Insert(T entity)
        {
            try
            {
                Table.Add(entity);
                int rowAffected = db.SaveChanges();
                return rowAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Insert(ICollection<T> entityCollection)
        {
            try
            {
                Table.AddRange(entityCollection);
                int rowAffected = db.SaveChanges();
                if (entityCollection.Count == rowAffected)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Edit(T entity)
        {
            try
            {
                Table.AddOrUpdate(entity);
                int rowAffected = db.SaveChanges();
                return rowAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                //var existingEntity = db.Entry(entity);
                //if (existingEntity != null && existingEntity.State != EntityState.Deleted)
                //{
                //    Table.Remove(entity);
                //}
                //else
                //{
                    //Table.Attach(entity);
                //var existingEntity = db.Entry(entity);
                dynamic eO = entity;
                var existingObj = Table.Find(eO.Id);
                if (existingObj != null)
                {
                    Table.Remove(existingObj);
                }
                
                //}

                int rowAffected = db.SaveChanges();
                return rowAffected > 0;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public T GetFirstOrDefaultBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return includes
                .Aggregate(
                    Table.AsNoTracking().AsQueryable(),
                    (current, include) => current.Include(include),
                    c => c.FirstOrDefault(predicate));
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
