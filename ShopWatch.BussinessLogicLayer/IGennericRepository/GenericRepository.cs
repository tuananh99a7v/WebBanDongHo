using ShopWatch.Model.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopWatch.BussinessLogicLayer.IGennericRepository
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		private ShopWatchDataContext _context;
		private readonly DbSet<TEntity> _dbSet;


		
		protected IDbFactory DbFactory { get; set; }
		protected ShopWatchDataContext DbContext => _context ?? (_context = DbFactory.Init());
		public GenericRepository(IDbFactory dbFactory)
		{
			DbFactory = dbFactory;
			_dbSet = DbContext.Set<TEntity>();
		}
		public virtual int Add(TEntity entity)
		{
			_dbSet.Add(entity);
			return _context.SaveChanges();
		}

		public virtual int Count()
		{
			return _dbSet.Count();
		}

		public Task<int> CountAsync()
		{
			throw new NotImplementedException();
		}

		public int Delete(TEntity entity)
		{
			if (_context.Entry(entity).State == EntityState.Detached)

			{
				_dbSet.Attach(entity);
			}
			_dbSet.Remove(entity);
			return _context.SaveChanges();
		}

		public IEnumerable<TEntity> DeleteBy(Expression<Func<TEntity, bool>> filter)
		{
			IEnumerable<TEntity> entities = _dbSet.Where(filter).AsEnumerable();
			_dbSet.RemoveRange(entities);
			_context.SaveChanges();
			return entities;
		}

		public TEntity Find(Expression<Func<TEntity, bool>> filter)
		{
			return _dbSet.Where(filter).FirstOrDefault();
		}

		public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter)
		{
			return _dbSet.Where(filter).ToList();
		}

		public Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter)
		{
			throw new NotImplementedException();
		}

		public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter)
		{
			throw new NotImplementedException();
		}

		public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> filter)
		{
            return _dbSet.Where(filter);

		}

		public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
		{
			IQueryable<TEntity> query = _dbSet;
			if (filter != null)
			{
				query = query.Where(filter);
			}
			foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}
			return orderBy != null ? orderBy(query) : query;
		}

		public IQueryable<TEntity> GetAll()
		{
			return _dbSet;
		}

		public Task<IEnumerable<TEntity>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public TEntity GetById(object id)
		{
			return _dbSet.Find(id);
		}

		public Task<TEntity> GetByIdAsync(object id)
		{
			throw new NotImplementedException();
		}

		public virtual void Update(TEntity entity)
		{
			_dbSet.AddOrUpdate(entity);
		}
	}
}
