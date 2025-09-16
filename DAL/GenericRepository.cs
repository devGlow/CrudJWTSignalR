
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Projet101.Models;
using System;
using System.Linq.Expressions;

namespace Projet101.DAL
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
         internal AppDbContext context;
           internal DbSet<TEntity> dbSet;

           public GenericRepository(AppDbContext context)
           {
               this.context = context;
               this.dbSet = context.Set<TEntity>();
           }





          public virtual IEnumerable<TEntity> Get(
             Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
          {

              IQueryable<TEntity> query = dbSet;

              if (filter != null)
              {
                  query = query.Where(filter);
              }


              if (includes!=null)
              {
                  query = includes(query);
              }

              if (orderBy != null)
              {
                  return orderBy(query).ToList();
              }
              else
              {
                  return query.ToList();
              }
          }


          public virtual TEntity GetByID(object id)
          {
              return dbSet.Find(id);
          }

          public virtual void Insert(TEntity entity)
          {
              dbSet.Add(entity);
          }


          public virtual void Update(TEntity entityToUpdate)
          {
              dbSet.Attach(entityToUpdate);
              context.Entry(entityToUpdate).State = EntityState.Modified;
          }

          public virtual void Delete(object id)
          {
              TEntity entityToDelete = dbSet.Find(id);
              Delete(entityToDelete);
          }

          public virtual void Delete(TEntity entityToDelete)
          {
              if (context.Entry(entityToDelete).State == EntityState.Detached)
              {
                  dbSet.Attach(entityToDelete);
              }
              dbSet.Remove(entityToDelete);
          }

      }
    
}
