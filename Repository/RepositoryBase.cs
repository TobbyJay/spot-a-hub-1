
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly AppDbContext _context;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
        }
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges ?
                 _context.Set<T>().AsNoTracking() :
                 _context.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ?
                 _context.Set<T>().Where(expression).AsNoTracking() :
                 _context.Set<T>().Where(expression);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void DeleteSingleId(Guid id)
        {

            var hub = _context.Hubbs.Single(o => o.HubbId == id);
            _context.Remove(hub);
            _context.SaveChanges();

        }
    }
}
