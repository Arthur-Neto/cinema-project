﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Theater.Domain.RoomsModule;
using Theater.Infra.Data.EF.Context;

namespace Theater.Infra.Data.EF.Repositories.RoomsModule
{
    public class RoomRepository : GenericRepositoryBase<Room, int>, IRoomRepository
    {
        public RoomRepository(IDatabaseContext context)
            : base(context)
        { }

        public Task<int> CountAsync(Expression<Func<Room, bool>> expression)
        {
            return GenericRepository.CountAsync(expression);
        }

        public Task<Room> CreateAsync(Room room)
        {
            return GenericRepository.CreateAsync(room);
        }

        public Task DeleteAsync(int key)
        {
            return GenericRepository.DeleteAsync(key);
        }

        public Task<IEnumerable<Room>> RetrieveAllAsync()
        {
            return GenericRepository.RetrieveAllAsync();
        }

        public Task<Room> SingleOrDefaultAsync(Expression<Func<Room, bool>> expression, bool tracking = true)
        {
            return GenericRepository.SingleOrDefaultAsync(expression, tracking);
        }

        public void Update(Room room)
        {
            GenericRepository.Update(room);
        }
    }
}