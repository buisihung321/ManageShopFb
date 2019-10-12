using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ManageShop.DataAccessLayer.DAL;
using ManageShop.DataAccessLayer.Infractructure.Contract;

namespace ManageShop.DataAccessLayer.Infractructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        public DbSet<T> dbSet { get; set; }
        private ManageShopContext dbContext;

        public BaseRepository()
        {
            dbContext = new ManageShopContext();
            this.dbSet = dbContext.Set<T>();
        }

        #region GenericMethod

        public T SingleOrDefault(Expression<Func<T, bool>> whereCondition)
        {
            var dbResult = dbSet.Where(whereCondition).FirstOrDefault();
            return dbResult;
        }


        public IEnumerable<T> GetAll()
        {
            return dbSet.AsEnumerable();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return dbSet.Where(whereCondition).AsEnumerable();
        }

        public virtual T Insert(T entity)
        {
            dynamic obj = dbSet.Add(entity);
            dbContext.SaveChanges();
            return obj;

        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();


        }

        public virtual void UpdateAll(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                dbSet.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
            }
            dbContext.SaveChanges();
        }

        public void Delete(Expression<Func<T, bool>> whereCondition)
        {
            IEnumerable<T> entities = this.GetAll(whereCondition);
            foreach (T entity in entities)
            {
                if (dbContext.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbSet.Remove(entity);
            }
            dbContext.SaveChanges();
        }

        public bool Exists(Expression<Func<T, bool>> whereCondition)
        {
            return dbSet.Any(whereCondition);
        }
        #endregion

    }
}
