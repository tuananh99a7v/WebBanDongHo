using ShopWatch.BussinessLogicLayer.IGennericRepository;
using ShopWatch.Model.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShopWatch.BussinessLogicLayer.Service
{
	public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        #region Fields

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IGenericRepository<TEntity> _repository;

        #endregion

        public BaseService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public virtual int Count()
        {
            return _repository.Count();
        }

        public virtual async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public virtual int Create(TEntity entity)
        {
           return _repository.Add(entity);
        }

       

        public virtual bool Delete(object id)
        {
            var entity = _repository.GetById(id);

            if (entity == null)
            {
                throw new Exception("Not found entity object with id: " + id);
            }
            _repository.Delete(entity);

            return _unitOfWork.Commit() > 0;

        }



        public virtual TEntity Find(Expression<Func<TEntity, bool>> filter)
        {
            return _repository.Find(filter);
        }

        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter)
        {
            return _repository.FindAll(filter);
        }

       

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _repository.FindAsync(filter);
        }

        public virtual TEntity FindInclude(
            Expression<Func<TEntity, bool>> filter,
            string includeProperties = "")
        {
            var query = _repository.FindBy(filter);

            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual PaginatedList<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int page = 1, int pageSize = 10)
        {
            var query = _repository.Get(filter: filter, includeProperties: includeProperties);
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return  PaginatedList<TEntity>.CreateAsync(query.AsNoTracking(), page, pageSize);
        }

        public virtual TEntity GetById(object id)
        {
            return _repository.GetById(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual bool Update(TEntity entity)
        {
            _repository.Update(entity);
            return _unitOfWork.Commit() > 0;

        }
        int IBaseService<TEntity>.CountAsync()
		{
			throw new NotImplementedException();
		}

		bool IBaseService<TEntity>.DeleteAsync(object id)
		{
			throw new NotImplementedException();
		}
	}
}
