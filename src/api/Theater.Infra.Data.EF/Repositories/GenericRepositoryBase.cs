﻿using System;
using Theater.Infra.Data.EF.Context;

namespace Theater.Infra.Data.EF.Repositories
{
    public abstract class GenericRepositoryBase<TEntity, KeyType> : IDisposable where TEntity : class
    {
        protected GenericRepository<TEntity, KeyType> GenericRepository;

        protected GenericRepositoryBase(IDatabaseContext context)
        {
            GenericRepository = new GenericRepository<TEntity, KeyType>(context);
        }

        public void Dispose()
        {
            GenericRepository.Dispose();
        }
    }
}
