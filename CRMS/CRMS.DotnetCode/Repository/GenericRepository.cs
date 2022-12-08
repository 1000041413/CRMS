using Data;
using Data.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private CRMSDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(CRMSDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }

        }

        public T Select(IFilter<T> filterClass)
        {
            return filterClass.Filter(_dbSet.AsQueryable()).First();
        }
        public IEnumerable<T> SelectMany(IFilter<T> filterClass)
        {
            return filterClass.Filter(_dbSet.AsQueryable());
        }
        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> queryableBase);
        }

        public Task<T> GetByID(long id) //async
        {
            return Task.FromResult<T>(null);
        }

        public void Insert(T entity) //async
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dbSet.Add(entity);
            _dbContext.SaveChanges();            
        }

        public Task  Delete(long id) //async
       {
            T existing = _dbSet.Find(id);
            _dbSet.Remove(existing);
            _dbContext.SaveChanges();
            return Task.FromResult<T>(null);
        }

		public void Delete(T entityToDelete)
        {
            // code
            if (entityToDelete == null)
            {
                throw new ArgumentNullException("entityToDelete");
            }
            _dbSet.Remove(entityToDelete);
            _dbContext.SaveChanges();

        }

		public void Update(T entityToUpdate)
        {
            if (entityToUpdate == null)
            {
                throw new ArgumentNullException("entityToUpdate");
            }
            _dbContext.SaveChanges();
        }

		public void Save(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (entity.Id > 0)
            {
                _dbSet.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            else
            {
                _dbSet.Add(entity);
                _dbContext.Entry(entity).State = EntityState.Added;
                _dbContext.SaveChanges();
            }
            
        }
	}
}
