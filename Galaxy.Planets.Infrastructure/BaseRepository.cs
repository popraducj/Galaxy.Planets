using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Galaxy.Planets.Core.Interfaces;
using Galaxy.Planets.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Galaxy.Planets.Infrastructure
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly PlanetsDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(PlanetsDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties == null) return orderBy != null ? orderBy(query).ToList() : query.ToList();

            foreach (var includeProperty in includeProperties.Split
                (new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }


            return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
        }

        public virtual TEntity GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<ActionResponse> AddAsync(TEntity entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new ActionResponse();
            }
            catch
            {
                return ActionResponse.FailedToAdd();
            }
        }
        public virtual async Task<ActionResponse> UpdateAsync(TEntity entityToUpdate)
        {
            try
            {
                _dbSet.Attach(entityToUpdate);
                _context.Entry(entityToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ActionResponse();
            }
            catch
            {
                return ActionResponse.FailedToUpdate();
            }
        }
    }
}