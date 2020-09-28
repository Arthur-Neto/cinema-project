using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Theater.Domain.SessionsModule;
using Theater.Infra.Data.EF.Context;

namespace Theater.Infra.Data.EF.Repositories.SessionsModule
{
    public class OccupiedChairRepository : GenericRepositoryBase<OccupiedChair, int>, IOccupiedChairRepository
    {
        public OccupiedChairRepository(IDatabaseContext context)
            : base(context)
        { }

        public Task<OccupiedChair> CreateAsync(OccupiedChair occupiedChair)
        {
            return GenericRepository.CreateAsync(occupiedChair);
        }

        public Task<OccupiedChair> SingleOrDefaultAsync(Expression<Func<OccupiedChair, bool>> expression, bool tracking = true, params Expression<Func<OccupiedChair, object>>[] includeExpression)
        {
            return GenericRepository.SingleOrDefaultAsync(expression, tracking, includeExpression);
        }

        public Task<IEnumerable<OccupiedChair>> RetrieveAllAsync(Expression<Func<OccupiedChair, bool>> expression = null, params Expression<Func<OccupiedChair, object>>[] includeExpression)
        {
            return GenericRepository.RetrieveAllAsync(expression, includeExpression);
        }
    }
}
