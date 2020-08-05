﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Theater.Domain;
using Theater.Infra.Data.EF.Context;

namespace Theater.Infra.Data.EF.Repositories
{
    public sealed class GenericRepository<TEntity, KeyType> :
        ICreateRepository<TEntity>,
        IRetrieveByIDRepository<TEntity, KeyType>,
        IRetrieveAllRepository<TEntity>,
        IUpdateRepository<TEntity>,
        IDeleteByIDRepository<TEntity, KeyType>,
        ISingleOrDefaultRepository<TEntity> where TEntity : class
    {
        public IDatabaseContext Context { get; private set; }

        public GenericRepository(IDatabaseContext context)
        {
            Context = context;
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var entityEntry = await Context.Set<TEntity>().AddAsync(entity);

            return entityEntry.Entity;
        }

        public async Task<TEntity> RetrieveByIDAsync(KeyType key)
        {
            return await Context.Set<TEntity>().FindAsync(key);
        }

        public async Task<IEnumerable<TEntity>> RetrieveAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(KeyType key)
        {
            var entities = Context.Set<TEntity>();
            var entity = await entities.FindAsync(key);
            entities.Remove(entity);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }
    }
}
