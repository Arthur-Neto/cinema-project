﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Theater.Domain
{
    public interface ICreateRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
    }

    public interface IRetrieveByIDRepository<TEntity, KeyType> : IDisposable where TEntity : class
    {
        Task<TEntity> RetrieveByIDAsync(KeyType key);
    }

    public interface IRetrieveAllRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<IEnumerable<TEntity>> RetrieveAllAsync();
    }

    public interface IUpdateRepository<TEntity> : IDisposable where TEntity : class
    {
        void Update(TEntity entity);
    }

    public interface IDeleteByIDRepository<TEntity, KeyType> : IDisposable where TEntity : class
    {
        Task DeleteAsync(KeyType key);
    }

    public interface ISingleOrDefaultRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
    }
}
